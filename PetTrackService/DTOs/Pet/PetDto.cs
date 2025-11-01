namespace PetTrackService.DTOs.Pet;

/// <summary>
/// Pet bilgilerini API'ye dönerken kullanýlan DTO.
/// Entity'yi dýþarýya expose etmez, sadece gerekli bilgileri içerir.
/// </summary>
public record PetDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Species { get; init; } = string.Empty;
    public string Breed { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public int Age { get; init; } // Hesaplanmýþ alan
    public string OwnerName { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public bool HasTracker { get; init; }
    public string? TrackerSerialNumber { get; init; }
}
