using System.ComponentModel.DataAnnotations;

namespace PetTrackService.DTOs.PetOwner;

public record CreatePetOwnerDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; init; }

  [StringLength(500)]
    public string Description { get; init; } = string.Empty;
}
