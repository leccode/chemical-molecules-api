using ChemicalMoleculeApi.Dtos;
using ChemicalMoleculeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChemicalMoleculeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoleculeController(IMoleculeService moleculeService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<MoleculeResponse>>>> GetMolecules()
        {
            var response = await moleculeService.GetAllMoleculesAsync();

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<MoleculeResponse>>> GetMolecule(int id)
        {
            var response = await moleculeService.GetMoleculeByIdAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<MoleculeResponse>>> AddMolecule(CreateMoleculeRequest molecule)
        {
            var response = await moleculeService.CreateMoleculeAsync(molecule);
            if (response.StatusCode is HttpStatusCode.Created && response.Result is not null)
            {
                return CreatedAtAction(nameof(GetMolecule),
                    new { id = response.Result.Id },
                    response);
            }

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateMolecule(int id, UpdateMoleculeRequest molecule)
        {
            var response = await moleculeService.UpdateMoleculeAsync(id, molecule);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteMolecule(int id)
        {
            var response = await moleculeService.DeleteMoleculeAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
