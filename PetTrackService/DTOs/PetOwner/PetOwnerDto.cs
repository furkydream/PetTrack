namespace PetTrackService.DTOs.PetOwner;

public record PetOwnerDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int PetCount { get; init; }
    public DateTime CreatedDate { get; init; }
}
