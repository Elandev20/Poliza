using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Servicios.api.poliza.Models;
using Servicios.api.poliza.Repositories;

namespace Servicios.api.poliza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolizaController : Controller
    {
        private IPolizaCollection db = new PolizaCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllPoliza()
        {
            return Ok(await db.GetAllPoliza());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolizaDetail(string id)
        {
            return Ok(await db.GetPolizaById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoliza([FromBody] Poliza poliza)
        {
            if (poliza == null)
            {
                return BadRequest();
            }

            if (poliza.NombrePoliza == string.Empty)
            {
                ModelState.AddModelError("NombrePoliza", "Nombre poliza is requited");
            }

            await db.InsertPoliza(poliza);

            return Created("Created", true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePoliza([FromBody] Poliza poliza, string id)
        {
            if (poliza == null)
            {
                return BadRequest();
            }

            if (poliza.NombrePoliza == string.Empty)
            {
                ModelState.AddModelError("NombrePoliza", "Nombre poliza is requited");
            }

            poliza.Id = new ObjectId(id);

            await db.UpdatePoliza(poliza);

            return Created("Updated", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePoliza(string id)
        {
            await db.DeletePoliza(id);
            return NoContent();
        }
    }
}
