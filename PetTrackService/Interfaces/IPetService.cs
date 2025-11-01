using PetTrackService.DTOs.Pet;

namespace PetTrackService.Interfaces;

/// <summary>
/// Pet ile ilgili tüm iþ mantýðý operasyonlarýný tanýmlar.
/// Controller katmaný bu arayüz üzerinden servis katmanýna eriþir.
/// </summary>
public interface IPetService
{
    Task<List<PetDto>> GetAllPetsAsync();
  Task<PetDto> GetPetByIdAsync(int id);
    Task<PetDetailDto> GetPetDetailAsync(int id);
    Task<List<PetDto>> GetPetsByOwnerIdAsync(int ownerId);
    Task<List<PetDto>> GetPetsBySpeciesAsync(string species);
    Task<PetDto> CreatePetAsync(CreatePetDto dto);
    Task<PetDto> UpdatePetAsync(int id, UpdatePetDto dto);
    Task<bool> DeletePetAsync(int id, bool hardDelete = false);
    Task<bool> AssignTrackerAsync(int petId, int trackerId);
    Task<bool> RemoveTrackerAsync(int petId);
}
