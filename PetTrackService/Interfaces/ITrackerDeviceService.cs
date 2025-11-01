using PetTrackService.DTOs.TrackerDevice;

namespace PetTrackService.Interfaces;

public interface ITrackerDeviceService
{
    Task<List<TrackerDeviceDto>> GetAllTrackersAsync();
    Task<TrackerDeviceDto> GetTrackerByIdAsync(int id);
    Task<TrackerDeviceDto> GetTrackerBySerialNumberAsync(string serialNumber);
    Task<List<TrackerDeviceDto>> GetUnassignedTrackersAsync();
    Task<TrackerDeviceDto> CreateTrackerAsync(CreateTrackerDeviceDto dto);
    Task<TrackerDeviceDto> UpdateLocationAsync(int id, UpdateTrackerLocationDto dto);
    Task<bool> DeleteTrackerAsync(int id, bool hardDelete = false);
}
