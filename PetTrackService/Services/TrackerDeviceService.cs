using PetTrackCore.Entites;
using PetTrackCore.Enums;
using PetTrackCore.ValueObjects;
using PetTrackDataAccess.UnitOfWork;
using PetTrackService.DTOs.TrackerDevice;
using PetTrackService.Exceptions;
using PetTrackService.Interfaces;

namespace PetTrackService.Services;

public class TrackerDeviceService : ITrackerDeviceService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrackerDeviceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<TrackerDeviceDto>> GetAllTrackersAsync()
    {
        var trackers = await _unitOfWork.trackerDevice.GetAllAsync();
     return trackers.Select(MapToDto).ToList();
    }

    public async Task<TrackerDeviceDto> GetTrackerByIdAsync(int id)
{
      var tracker = await _unitOfWork.trackerDevice.GetByIdAsync(id);
        if (tracker == null)
  throw new NotFoundException(nameof(TrackerDevice), id);

        return MapToDto(tracker);
    }

    public async Task<TrackerDeviceDto> GetTrackerBySerialNumberAsync(string serialNumber)
    {
        var allTrackers = await _unitOfWork.trackerDevice.GetAllAsync();
        var tracker = allTrackers.FirstOrDefault(t => 
     t.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));

 if (tracker == null)
 throw new NotFoundException($"Tracker with serial number '{serialNumber}' not found");

        return MapToDto(tracker);
    }

  public async Task<List<TrackerDeviceDto>> GetUnassignedTrackersAsync()
    {
        var allTrackers = await _unitOfWork.trackerDevice.GetAllAsync();
        return allTrackers
  .Where(t => t.PetId == 0)
     .Select(MapToDto)
    .ToList();
    }

    public async Task<TrackerDeviceDto> CreateTrackerAsync(CreateTrackerDeviceDto dto)
    {
    // Ýþ kuralý: Ayný seri numarasýnda tracker var mý?
        var allTrackers = await _unitOfWork.trackerDevice.GetAllAsync();
   var existingTracker = allTrackers.FirstOrDefault(t => 
     t.SerialNumber.Equals(dto.SerialNumber, StringComparison.OrdinalIgnoreCase));

        if (existingTracker != null)
        throw new ValidationException($"Bu seri numarasýnda tracker zaten mevcut: {dto.SerialNumber}");

   // Eðer PetId verilmiþse, Pet'in var olduðunu kontrol et
if (dto.PetId.HasValue)
        {
      var pet = await _unitOfWork.Pet.GetByIdAsync(dto.PetId.Value);
  if (pet == null)
       throw new ValidationException($"Pet bulunamadý: {dto.PetId}");
        }

var tracker = new TrackerDevice
        {
   SerialNumber = dto.SerialNumber,
            BatteryLevel = dto.BatteryLevel,
 Location = new Location
  {
          Enlem = dto.Latitude,
    Boylam = dto.Longitude
  },
            PetId = dto.PetId ?? 0,
        CreatedDate = DateTime.UtcNow,
     Status = EntityStatus.Active
     };

        await _unitOfWork.trackerDevice.AddAsync(tracker);
        await _unitOfWork.CommitAsync();

     return MapToDto(tracker);
    }

    public async Task<TrackerDeviceDto> UpdateLocationAsync(int id, UpdateTrackerLocationDto dto)
    {
  var tracker = await _unitOfWork.trackerDevice.GetByIdAsync(id);
        if (tracker == null)
      throw new NotFoundException(nameof(TrackerDevice), id);

        tracker.Location = new Location
  {
     Enlem = dto.Latitude,
      Boylam = dto.Longitude
        };

     if (dto.BatteryLevel.HasValue)
  {
            tracker.BatteryLevel = dto.BatteryLevel.Value;
        }

  await _unitOfWork.trackerDevice.UpdateAsync(tracker);
    await _unitOfWork.CommitAsync();

  return MapToDto(tracker);
 }

    public async Task<bool> DeleteTrackerAsync(int id, bool hardDelete = false)
    {
var tracker = await _unitOfWork.trackerDevice.GetByIdAsync(id);
  if (tracker == null)
     throw new NotFoundException(nameof(TrackerDevice), id);

        // Ýþ kuralý: Tracker bir Pet'e baðlýysa uyarý ver
 if (tracker.PetId != 0 && !hardDelete)
        {
     throw new BusinessException("Tracker bir Pet'e baðlý. Önce iliþkiyi kaldýrýn veya hardDelete kullanýn.");
    }

  var result = await _unitOfWork.trackerDevice.DeleteAsync(id, hardDelete);
 await _unitOfWork.CommitAsync();

   return result;
    }

    private TrackerDeviceDto MapToDto(TrackerDevice tracker)
    {
        return new TrackerDeviceDto
      {
            Id = tracker.Id,
         SerialNumber = tracker.SerialNumber,
  BatteryLevel = tracker.BatteryLevel,
    Latitude = tracker.Location.Enlem,
     Longitude = tracker.Location.Boylam,
  PetId = tracker.PetId != 0 ? tracker.PetId : null,
     PetName = tracker.Pet?.Name,
      CreatedDate = tracker.CreatedDate
      };
    }
}
