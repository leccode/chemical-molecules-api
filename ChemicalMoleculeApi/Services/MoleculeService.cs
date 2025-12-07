using ChemicalMoleculeApi.Data;
using ChemicalMoleculeApi.Dtos;
using ChemicalMoleculeApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ChemicalMoleculeApi.Services
{
    public class MoleculeService(AppDbContext context) : IMoleculeService
    {
        public async Task<ApiResponse<List<MoleculeResponse>>> GetAllMoleculesAsync()
        {
            try
            {
                var molecules = await context.Molecules.Select(molecule => new MoleculeResponse
                {
                    Id = molecule.Id,
                    Name = molecule.Name,
                    Description = molecule.Description,
                    ScientificName = molecule.ScientificName,
                    Formula = molecule.Formula,
                    MolecularWeight = molecule.MolecularWeight
                }).ToListAsync();

                return new ApiResponse<List<MoleculeResponse>>
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = molecules
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<MoleculeResponse>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = { ex.Message },
                    Result = null
                };
            }
        }

        public async Task<ApiResponse<MoleculeResponse?>> GetMoleculeByIdAsync(int id)
        {
            try
            {
                if (id is <= 0)
                {
                    return new ApiResponse<MoleculeResponse?>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                        ErrorMessages = { "Id should be greater than zero." },
                        Result = null
                    };
                }

                var molecule = await context.Molecules
                    .Where(molecule => molecule.Id == id)
                    .Select(molecule => new MoleculeResponse
                    {
                        Id = molecule.Id,
                        Name = molecule.Name,
                        Description = molecule.Description,
                        ScientificName = molecule.ScientificName,
                        Formula = molecule.Formula,
                        MolecularWeight = molecule.MolecularWeight
                    }).FirstOrDefaultAsync();

                if (molecule is null)
                {
                    return new ApiResponse<MoleculeResponse?>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = { "Molecule not found." },
                        Result = null
                    };
                }

                return new ApiResponse<MoleculeResponse?>
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = molecule
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<MoleculeResponse?>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = { ex.Message },
                    Result = null
                };
            }
        }

        public async Task<ApiResponse<MoleculeResponse>> CreateMoleculeAsync(CreateMoleculeRequest molecule)
        {
            try
            {
                var newMolecule = new Molecule
                {
                    Name = molecule.Name,
                    Description = molecule.Description,
                    ScientificName = molecule.ScientificName,
                    Formula = molecule.Formula,
                    MolecularWeight = molecule.MolecularWeight
                };

                context.Molecules.Add(newMolecule);
                await context.SaveChangesAsync();

                var createdMolecule = new MoleculeResponse
                {
                    Id = newMolecule.Id,
                    Name = newMolecule.Name,
                    Description = newMolecule.Description,
                    ScientificName = newMolecule.ScientificName,
                    Formula = newMolecule.Formula,
                    MolecularWeight = newMolecule.MolecularWeight
                };

                return new ApiResponse<MoleculeResponse>
                {
                    StatusCode = HttpStatusCode.Created,
                    IsSuccess = true,
                    Result = createdMolecule
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<MoleculeResponse>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = { ex.Message },
                    Result = null
                };
            }
        }

        public async Task<ApiResponse<bool>> UpdateMoleculeAsync(int id, UpdateMoleculeRequest molecule)
        {
            try
            {
                if (id is <= 0)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                        ErrorMessages = { "Id should be greater than zero." },
                        Result = false
                    };
                }

                var existingMolecule = await context.Molecules.FindAsync(id);
                if (existingMolecule is null)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = { "Molecule not found." },
                        Result = false
                    };
                }

                existingMolecule.Name = molecule.Name;
                existingMolecule.Description = molecule.Description;
                existingMolecule.ScientificName = molecule.ScientificName;
                existingMolecule.Formula = molecule.Formula;
                existingMolecule.MolecularWeight = molecule.MolecularWeight;
                await context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = { ex.Message },
                    Result = false
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteMoleculeAsync(int id)
        {
            try
            {
                if (id is <= 0)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        IsSuccess = false,
                        ErrorMessages = { "Id should be greater than zero." },
                        Result = false
                    };
                }

                var moleculeToDelete = await context.Molecules.FindAsync(id);
                if (moleculeToDelete is null)
                {
                    return new ApiResponse<bool>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        ErrorMessages = { "Molecule not found." },
                        Result = false
                    };
                }

                context.Molecules.Remove(moleculeToDelete);
                await context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = { ex.Message },
                    Result = false
                };
            }
        }
    }
}
