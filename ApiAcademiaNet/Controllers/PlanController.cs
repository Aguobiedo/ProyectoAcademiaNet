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
    public class PlanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanes()
        {
            var planes = await _context.Plan
                .Include(p => p.Especialidad) // Incluye la especialidad asociada
                .ToListAsync();
            return Ok(new ResponseDto { Data = planes });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan(int id)
        {
            var plan = await _context.Plan
                .Include(p => p.Especialidad) // Incluye la especialidad asociada
                .FirstOrDefaultAsync(p => p.IdPlan == id);
            if (plan == null)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Plan no encontrado" });
            }

            return Ok(new ResponseDto { Data = plan });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] Plan plan)
        {
            // Verificar si la especialidad ya existe en la base de datos
            var especialidad = await _context.Especialidad.FindAsync(plan.IdEspecialidad);
            if (especialidad == null)
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "Especialidad no encontrada" });
            }

            // Asociar la especialidad al plan
            plan.Especialidad = especialidad;

            _context.Plan.Add(plan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlan), new { id = plan.IdPlan }, new ResponseDto { Data = plan });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] Plan updatedPlan)
        {
            if (id != updatedPlan.IdPlan)
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "ID no coincide" });
            }

            _context.Entry(updatedPlan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
                {
                    return NotFound(new ResponseDto { IsSuccess = false, Message = "Plan no encontrado" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var plan = await _context.Plan.FindAsync(id);
            if (plan == null)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Plan no encontrado" });
            }

            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.IdPlan == id);
        }
    }
}
