using ChemicalMoleculeApi.Dtos;

namespace ChemicalMoleculeApi.Services
{
    public interface IMoleculeService
    {
        Task<ApiResponse<List<MoleculeResponse>>> GetAllMoleculesAsync();
        Task<ApiResponse<MoleculeResponse?>> GetMoleculeByIdAsync(int id);
        Task<ApiResponse<MoleculeResponse>> CreateMoleculeAsync(CreateMoleculeRequest molecule);
        Task<ApiResponse<bool>> UpdateMoleculeAsync(int id, UpdateMoleculeRequest molecule);
        Task<ApiResponse<bool>> DeleteMoleculeAsync(int id);
    }
}
