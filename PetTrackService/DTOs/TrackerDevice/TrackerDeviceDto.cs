namespace PetTrackService.DTOs.TrackerDevice;

public record TrackerDeviceDto
{
    public int Id { get; init; }
    public string SerialNumber { get; init; } = string.Empty;
    public double BatteryLevel { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public int? PetId { get; init; }
    public string? PetName { get; init; }
    public DateTime CreatedDate { get; init; }
}
