using PetTrackService.DTOs.Pet;

namespace PetTrackService.DTOs.PetOwner;

public record PetOwnerDetailDto
{
    public int Id { get; init; }
  public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public List<PetDto> Pets { get; init; } = new();
}
