using System.ComponentModel.DataAnnotations;

namespace PetTrackService.DTOs.TrackerDevice;

public record CreateTrackerDeviceDto
{
 [Required(ErrorMessage = "Seri numarasý zorunludur")]
    [StringLength(50, ErrorMessage = "Seri numarasý maksimum 50 karakter olabilir")]
    public required string SerialNumber { get; init; }

    [Range(0, 100, ErrorMessage = "Batarya seviyesi 0-100 arasý olmalýdýr")]
    public double BatteryLevel { get; init; } = 100;

    [Range(-90, 90, ErrorMessage = "Geçerli bir enlem giriniz")]
    public double Latitude { get; init; }

    [Range(-180, 180, ErrorMessage = "Geçerli bir boylam giriniz")]
    public double Longitude { get; init; }

    public int? PetId { get; init; }
}
