using System.ComponentModel.DataAnnotations;

namespace PetTrackService.DTOs.TrackerDevice;

public record UpdateTrackerLocationDto
{
    [Range(-90, 90)]
    public required double Latitude { get; init; }

    [Range(-180, 180)]
    public required double Longitude { get; init; }

    [Range(0, 100)]
    public double? BatteryLevel { get; init; }
}