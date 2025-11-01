using System.ComponentModel.DataAnnotations;

namespace PetTrackService.DTOs.Pet;

public record UpdatePetDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public required string Name { get; init; }

    [Required]
    [StringLength(100)]
    public required string Breed { get; init; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime DateOfBirth { get; init; }
}
