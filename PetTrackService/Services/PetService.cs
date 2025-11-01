using PetTrackCore.Entites;
using PetTrackCore.Enums;
using PetTrackDataAccess.UnitOfWork;
using PetTrackService.DTOs.Pet;
using PetTrackService.Exceptions;
using PetTrackService.Interfaces;

namespace PetTrackService.Services;

public class PetService : IPetService
{
    private readonly IUnitOfWork _unitOfWork;

    public PetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PetDto>> GetAllPetsAsync()
    {
  var pets = await _unitOfWork.Pet.GetAllAsync();
        return pets.Select(MapToDto).ToList();
 }

    public async Task<PetDto> GetPetByIdAsync(int id)
 {
 var pet = await _unitOfWork.Pet.GetByIdAsync(id);
     if (pet == null)
    throw new NotFoundException(nameof(Pet), id);

    return MapToDto(pet);
    }

    public async Task<PetDetailDto> GetPetDetailAsync(int id)
  {
        var pet = await _unitOfWork.Pet.GetByIdAsync(id);
      if (pet == null)
   throw new NotFoundException(nameof(Pet), id);

        return new PetDetailDto
        {
            Id = pet.Id,
  Name = pet.Name,
            Species = pet.Species,
  Breed = pet.Breed,
 DateOfBirth = pet.DateOfBirth,
       Age = CalculateAge(pet.DateOfBirth),
  OwnerName = pet.PetOwner?.Name ?? "Bilinmiyor",
    CreatedDate = pet.CreatedDate,
 TrackerDevice = pet.TrackerDevice != null ? new TrackerDeviceDto
 {
    Id = pet.TrackerDevice.Id,
    SerialNumber = pet.TrackerDevice.SerialNumber,
                BatteryLevel = pet.TrackerDevice.BatteryLevel,
          Latitude = pet.TrackerDevice.Location.Enlem,
       Longitude = pet.TrackerDevice.Location.Boylam
      } : null,
   HealthRecords = pet.HealthRecords
        .OrderByDescending(h => h.RecordDate)
 .Take(5)
      .Select(h => new HealthRecordDto
     {
       Id = h.Id,
    RecordDate = h.RecordDate,
           Description = h.Description,
     RecordType = h.RecordType.ToString(),
         VaccineName = h.VaccineName
          }).ToList(),
        UpcomingAppointments = pet.VetAppointments
       .Where(v => v.AppointmentDate >= DateTime.Now)
  .OrderBy(v => v.AppointmentDate)
                .Select(v => new VetAppointmentDto
       {
        Id = v.Id,
   AppointmentDate = v.AppointmentDate,
        Description = v.Description
    }).ToList(),
 RecentActivities = pet.ActivityLogs
       .OrderByDescending(a => a.ActivityDate)
    .Take(10)
      .Select(a => new ActivityLogDto
  {
  Id = a.Id,
     ActivityDate = a.ActivityDate,
   ActivityType = a.ActivityType.ToString(),
        Description = a.Description,
       Distance = a.Distance,
       Duration = a.Duration
    }).ToList(),
    ActiveAlerts = pet.Alerts
  .Where(a => a.Status == EntityStatus.Active)
    .OrderByDescending(a => a.AlertDate)
    .Select(a => new AlertDto
   {
      Id = a.Id,
   AlertDate = a.AlertDate,
      AlertType = a.AlertType.ToString(),
      Message = a.Message
    }).ToList()
        };
    }

    public async Task<List<PetDto>> GetPetsByOwnerIdAsync(int ownerId)
    {
 var owner = await _unitOfWork.PetOwner.GetByIdAsync(ownerId);
     if (owner == null)
          throw new NotFoundException(nameof(PetOwner), ownerId);

        var allPets = await _unitOfWork.Pet.GetAllAsync();
        return allPets
         .Where(p => p.PetOwnerId == ownerId)
          .Select(MapToDto)
       .ToList();
    }

    public async Task<List<PetDto>> GetPetsBySpeciesAsync(string species)
    {
        var allPets = await _unitOfWork.Pet.GetAllAsync();
   return allPets
            .Where(p => p.Species.Equals(species, StringComparison.OrdinalIgnoreCase))
 .Select(MapToDto)
    .ToList();
  }

    public async Task<PetDto> CreatePetAsync(CreatePetDto dto)
    {
        // Ýþ kuralý: Sahip var mý kontrolü
      var owner = await _unitOfWork.PetOwner.GetByIdAsync(dto.PetOwnerId);
        if (owner == null)
            throw new ValidationException($"Sahip bulunamadý: {dto.PetOwnerId}");

        // Ýþ kuralý: Doðum tarihi gelecekte olamaz
    if (dto.DateOfBirth > DateTime.Now)
      throw new ValidationException("Doðum tarihi gelecekte olamaz");

        var pet = new Pet
        {
   Name = dto.Name,
   Species = dto.Species,
   Breed = dto.Breed,
       DateOfBirth = dto.DateOfBirth,
            PetOwnerId = dto.PetOwnerId,
   CreatedDate = DateTime.UtcNow,
      Status = EntityStatus.Active
   };

   await _unitOfWork.Pet.AddAsync(pet);
        await _unitOfWork.CommitAsync();

    return MapToDto(pet);
    }

    public async Task<PetDto> UpdatePetAsync(int id, UpdatePetDto dto)
    {
 var pet = await _unitOfWork.Pet.GetByIdAsync(id);
        if (pet == null)
      throw new NotFoundException(nameof(Pet), id);

        if (dto.DateOfBirth > DateTime.Now)
    throw new ValidationException("Doðum tarihi gelecekte olamaz");

        pet.Name = dto.Name;
        pet.Breed = dto.Breed;
     pet.DateOfBirth = dto.DateOfBirth;

      await _unitOfWork.Pet.UpdateAsync(pet);
  await _unitOfWork.CommitAsync();

return MapToDto(pet);
    }

public async Task<bool> DeletePetAsync(int id, bool hardDelete = false)
    {
  var pet = await _unitOfWork.Pet.GetByIdAsync(id);
      if (pet == null)
    throw new NotFoundException(nameof(Pet), id);

// Ýþ kuralý: Pet'e baðlý aktif tracker varsa silinmemeli (opsiyonel)
 if (pet.TrackerDevice != null && !hardDelete)
      {
            throw new BusinessException("Pet'e baðlý tracker var. Önce tracker'ý kaldýrýn.");
      }

        var result = await _unitOfWork.Pet.DeleteAsync(id, hardDelete);
      await _unitOfWork.CommitAsync();

        return result;
    }

  public async Task<bool> AssignTrackerAsync(int petId, int trackerId)
    {
      var pet = await _unitOfWork.Pet.GetByIdAsync(petId);
if (pet == null)
            throw new NotFoundException(nameof(Pet), petId);

        var tracker = await _unitOfWork.trackerDevice.GetByIdAsync(trackerId);
    if (tracker == null)
          throw new NotFoundException(nameof(TrackerDevice), trackerId);

      // Ýþ kuralý: Tracker zaten baþka bir Pet'e atanmýþ mý?
 var allTrackers = await _unitOfWork.trackerDevice.GetAllAsync();
    var assignedTracker = allTrackers.FirstOrDefault(t => t.Id == trackerId && t.PetId != 0);
 if (assignedTracker != null)
      throw new BusinessException($"Tracker zaten baþka bir Pet'e atanmýþ (Pet ID: {assignedTracker.PetId})");

tracker.PetId = petId;
     await _unitOfWork.trackerDevice.UpdateAsync(tracker);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RemoveTrackerAsync(int petId)
  {
        var pet = await _unitOfWork.Pet.GetByIdAsync(petId);
        if (pet == null)
 throw new NotFoundException(nameof(Pet), petId);

 if (pet.TrackerDevice == null)
            throw new BusinessException("Bu Pet'in tracker'ý yok");

    var tracker = pet.TrackerDevice;
    tracker.PetId = 0; // Ýliþkiyi kopar
        await _unitOfWork.trackerDevice.UpdateAsync(tracker);
        await _unitOfWork.CommitAsync();

        return true;
    }

 // Helper Methods
    private PetDto MapToDto(Pet pet)
{
        return new PetDto
        {
       Id = pet.Id,
  Name = pet.Name,
  Species = pet.Species,
    Breed = pet.Breed,
            DateOfBirth = pet.DateOfBirth,
    Age = CalculateAge(pet.DateOfBirth),
         OwnerName = pet.PetOwner?.Name ?? "Bilinmiyor",
       CreatedDate = pet.CreatedDate,
            HasTracker = pet.TrackerDevice != null,
  TrackerSerialNumber = pet.TrackerDevice?.SerialNumber
     };
    }

    private int CalculateAge(DateTime dateOfBirth)
    {
  var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
 if (dateOfBirth.Date > today.AddYears(-age))
       age--;
  return age;
    }
}
