using ChemicalMoleculeApi.Dtos;

namespace ChemicalMoleculeApi.Services
{
    public interface IMoleculeService
    {
        Task<List<MoleculeResponse>> GetAllMoleculesAsync();
        Task<MoleculeResponse?> GetMoleculeByIdAsync(int id);
        Task<MoleculeResponse> CreateMoleculeAsync(CreateMoleculeRequest molecule);
        Task<bool> UpdateMoleculeAsync(int id, UpdateMoleculeRequest molecule);
        Task<bool> DeleteMoleculeAsync(int id);
    }
}
