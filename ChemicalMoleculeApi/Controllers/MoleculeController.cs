using ChemicalMoleculeApi.Dtos;
using ChemicalMoleculeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChemicalMoleculeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoleculeController(IMoleculeService moleculeService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MoleculeResponse>>> GetMolecules()
            => Ok(await moleculeService.GetAllMoleculesAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MoleculeResponse>> GetMolecule(int id)
        {
            var molecule = await moleculeService.GetMoleculeByIdAsync(id);

            return molecule is null
                ? NotFound("Molecule with given Id was not found.")
                : Ok(molecule);
        }

        [HttpPost]
        public async Task<ActionResult<MoleculeResponse>> AddMolecule(CreateMoleculeRequest molecule)
        {
            var createdMolecule = await moleculeService.CreateMoleculeAsync(molecule);

            return CreatedAtAction(nameof(GetMolecule), new { id = createdMolecule.Id }, createdMolecule);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateMolecule(int id, UpdateMoleculeRequest molecule)
        {
            var isUpdated = await moleculeService.UpdateMoleculeAsync(id, molecule);

            return isUpdated
                ? NoContent()
                : NotFound("Molecule with given Id was not found.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMolecule(int id)
        {
            var isDeleted = await moleculeService.DeleteMoleculeAsync(id);

            return isDeleted
                ? NoContent()
                : NotFound("Molecule with given Id was not found.");
        }
    }
}
