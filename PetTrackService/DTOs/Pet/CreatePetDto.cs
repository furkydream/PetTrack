using System.ComponentModel.DataAnnotations;

namespace PetTrackService.DTOs.Pet;

/// <summary>
/// Yeni Pet oluþtururken kullanýlan DTO.
/// Validation attribute'larý ile doðrulama yapýlýr.
/// </summary>
public record CreatePetDto
{
    [Required(ErrorMessage = "Pet adý zorunludur")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Pet adý 2-100 karakter arasý olmalýdýr")]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Tür bilgisi zorunludur")]
    [StringLength(50, ErrorMessage = "Tür bilgisi maksimum 50 karakter olabilir")]
    public required string Species { get; init; }

    [Required(ErrorMessage = "Cins bilgisi zorunludur")]
    [StringLength(100, ErrorMessage = "Cins bilgisi maksimum 100 karakter olabilir")]
    public required string Breed { get; init; }

    [Required(ErrorMessage = "Doðum tarihi zorunludur")]
    [DataType(DataType.Date)]
    public required DateTime DateOfBirth { get; init; }

    [Required(ErrorMessage = "Sahip ID'si zorunludur")]
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir sahip ID'si giriniz")]
    public required int PetOwnerId { get; init; }
}
