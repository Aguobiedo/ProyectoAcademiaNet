using ApiAcademiaNet.Data;
using ApiAcademiaNet.Models;
using ApiAcademiaNet.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiAcademiaNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EspecialidadController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEspecialidades()
        {
            var especialidades = await _context.Especialidad.ToListAsync();
            return Ok(new ResponseDto { Data = especialidades });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialidad(int id)
        {
            var especialidad = await _context.Especialidad.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Especialidad no encontrada" });
            }

            return Ok(new ResponseDto { Data = especialidad });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEspecialidad([FromBody] Especialidad especialidad)
        {
            _context.Especialidad.Add(especialidad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEspecialidad), new { id = especialidad.IdEspecialidad }, new ResponseDto { Data = especialidad });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEspecialidad(int id, [FromBody] Especialidad updatedEspecialidad)
        {
            if (id != updatedEspecialidad.IdEspecialidad)
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "ID no coincide" });
            }

            _context.Entry(updatedEspecialidad).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadExists(id))
                {
                    return NotFound(new ResponseDto { IsSuccess = false, Message = "Especialidad no encontrada" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var especialidad = await _context.Especialidad.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Especialidad no encontrada" });
            }

            _context.Especialidad.Remove(especialidad);
            await _context.SaveChangesAsync();  
            return NoContent();
        }

        private bool EspecialidadExists(int id)
        {
            return _context.Especialidad.Any(e => e.IdEspecialidad == id);
        }
    }
}
