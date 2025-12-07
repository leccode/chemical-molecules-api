using ChemicalMoleculeApi.Data;
using ChemicalMoleculeApi.Dtos;
using ChemicalMoleculeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChemicalMoleculeApi.Services
{
    public class MoleculeService(AppDbContext context) : IMoleculeService
    {
        public async Task<List<MoleculeResponse>> GetAllMoleculesAsync() =>
            await context.Molecules.Select(molecule => new MoleculeResponse
            {
                Id = molecule.Id,
                Name = molecule.Name,
                Description = molecule.Description,
                ScientificName = molecule.ScientificName,
                Formula = molecule.Formula,
                MolecularWeight = molecule.MolecularWeight
            }).ToListAsync();

        public async Task<MoleculeResponse?> GetMoleculeByIdAsync(int id) =>
            await context.Molecules
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
        
        public async Task<MoleculeResponse> CreateMoleculeAsync(CreateMoleculeRequest molecule)
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

            return new MoleculeResponse
            {
                Id = newMolecule.Id,
                Name = newMolecule.Name,
                Description = newMolecule.Description,
                ScientificName = newMolecule.ScientificName,
                Formula = newMolecule.Formula,
                MolecularWeight = newMolecule.MolecularWeight
            };
        }
        
        public async Task<bool> UpdateMoleculeAsync(int id, UpdateMoleculeRequest molecule)
        {
            var existingMolecule = await context.Molecules.FindAsync(id);
            if (existingMolecule is null) return false;

            existingMolecule.Name = molecule.Name;
            existingMolecule.Description = molecule.Description;
            existingMolecule.ScientificName = molecule.ScientificName;
            existingMolecule.Formula = molecule.Formula;
            existingMolecule.MolecularWeight = molecule.MolecularWeight;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMoleculeAsync(int id)
        {
            var moleculeToDelete = await context.Molecules.FindAsync(id);
            if (moleculeToDelete is null) return false;

            context.Molecules.Remove(moleculeToDelete);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
