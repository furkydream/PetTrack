using PetTrackCore.Entites;
using PetTrackCore.Enums;
using PetTrackDataAccess.UnitOfWork;
using PetTrackService.DTOs.Pet;
using PetTrackService.DTOs.PetOwner;
using PetTrackService.Exceptions;
using PetTrackService.Interfaces;

namespace PetTrackService.Services;

public class PetOwnerService : IPetOwnerService
{
 private readonly IUnitOfWork _unitOfWork;

    public PetOwnerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
  }

    public async Task<List<PetOwnerDto>> GetAllOwnersAsync()
    {
        var owners = await _unitOfWork.PetOwner.GetAllAsync();
     return owners.Select(MapToDto).ToList();
    }

public async Task<PetOwnerDto> GetOwnerByIdAsync(int id)
    {
     var owner = await _unitOfWork.PetOwner.GetByIdAsync(id);
        if (owner == null)
   throw new NotFoundException(nameof(PetOwner), id);

        return MapToDto(owner);
    }

    public async Task<PetOwnerDetailDto> GetOwnerDetailAsync(int id)
    {
var owner = await _unitOfWork.PetOwner.GetByIdAsync(id);
  if (owner == null)
       throw new NotFoundException(nameof(PetOwner), id);

        return new PetOwnerDetailDto
   {
    Id = owner.Id,
        Name = owner.Name,
         Description = owner.Description,
      CreatedDate = owner.CreatedDate,
    Pets = owner.Pets.Select(p => new PetDto
      {
     Id = p.Id,
     Name = p.Name,
   Species = p.Species,
       Breed = p.Breed,
       DateOfBirth = p.DateOfBirth,
   Age = CalculateAge(p.DateOfBirth),
  OwnerName = owner.Name,
    CreatedDate = p.CreatedDate,
  HasTracker = p.TrackerDevice != null,
        TrackerSerialNumber = p.TrackerDevice?.SerialNumber
     }).ToList()
   };
    }

  public async Task<PetOwnerDto> CreateOwnerAsync(CreatePetOwnerDto dto)
  {
var owner = new PetOwner
      {
   Name = dto.Name,
        Description = dto.Description,
 CreatedDate = DateTime.UtcNow,
    Status = EntityStatus.Active
     };

    await _unitOfWork.PetOwner.AddAsync(owner);
        await _unitOfWork.CommitAsync();

        return MapToDto(owner);
    }

    public async Task<PetOwnerDto> UpdateOwnerAsync(int id, CreatePetOwnerDto dto)
    {
        var owner = await _unitOfWork.PetOwner.GetByIdAsync(id);
     if (owner == null)
   throw new NotFoundException(nameof(PetOwner), id);

      owner.Name = dto.Name;
    owner.Description = dto.Description;

        await _unitOfWork.PetOwner.UpdateAsync(owner);
 await _unitOfWork.CommitAsync();

   return MapToDto(owner);
    }

    public async Task<bool> DeleteOwnerAsync(int id, bool hardDelete = false)
    {
 var owner = await _unitOfWork.PetOwner.GetByIdAsync(id);
     if (owner == null)
   throw new NotFoundException(nameof(PetOwner), id);

    // Ýþ kuralý: Pet'leri olan sahip silinmemeli (soft delete bile)
        if (owner.Pets.Any() && !hardDelete)
     {
            throw new BusinessException("Bu sahibin Pet'leri var. Önce Pet'leri silin veya baþka bir sahibesahip atayýn.");
      }

        var result = await _unitOfWork.PetOwner.DeleteAsync(id, hardDelete);
    await _unitOfWork.CommitAsync();

   return result;
    }

 private PetOwnerDto MapToDto(PetOwner owner)
    {
   return new PetOwnerDto
  {
       Id = owner.Id,
      Name = owner.Name,
  Description = owner.Description,
  PetCount = owner.Pets?.Count ?? 0,
 CreatedDate = owner.CreatedDate
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
