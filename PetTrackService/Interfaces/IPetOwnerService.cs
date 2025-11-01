using PetTrackService.DTOs.PetOwner;

namespace PetTrackService.Interfaces;

public interface IPetOwnerService
{
    Task<List<PetOwnerDto>> GetAllOwnersAsync();
    Task<PetOwnerDto> GetOwnerByIdAsync(int id);
    Task<PetOwnerDetailDto> GetOwnerDetailAsync(int id);
    Task<PetOwnerDto> CreateOwnerAsync(CreatePetOwnerDto dto);
    Task<PetOwnerDto> UpdateOwnerAsync(int id, CreatePetOwnerDto dto);
    Task<bool> DeleteOwnerAsync(int id, bool hardDelete = false);
}
