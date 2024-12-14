using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAcademiaNet.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAcademiaNet.Data;

namespace ApiAcademiaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComisionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComisionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Comision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comision>>> GetComisiones()
        {
            return await _context.Comision.Include(c => c.Plan).ToListAsync();
        }

        // GET: api/Comision/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comision>> GetComision(int id)
        {
            var comision = await _context.Comision.Include(c => c.Plan)
                                                  .FirstOrDefaultAsync(c => c.IdComision == id);

            if (comision == null)
            {
                return NotFound();
            }

            return comision;
        }

        // POST: api/Comision
        [HttpPost]
        public async Task<ActionResult<Comision>> PostComision(Comision comision)
        {
            // Verificar que el Plan existe
            var planExists = await _context.Plan.AnyAsync(p => p.IdPlan == comision.IdPlan);
            if (!planExists)
            {
                return BadRequest("El Plan especificado no existe.");
            }

            _context.Comision.Add(comision);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComision), new { id = comision.IdComision }, comision);
        }

        // PUT: api/Comision/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComision(int id, Comision comision)
        {
            if (id != comision.IdComision)
            {
                return BadRequest();
            }

            // Verificar que el Plan existe
            var planExists = await _context.Plan.AnyAsync(p => p.IdPlan == comision.IdPlan);
            if (!planExists)
            {
                return BadRequest("El Plan especificado no existe.");
            }

            _context.Entry(comision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComisionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Comision/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComision(int id)
        {
            var comision = await _context.Comision.FindAsync(id);
            if (comision == null)
            {
                return NotFound();
            }

            _context.Comision.Remove(comision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComisionExists(int id)
        {
            return _context.Comision.Any(e => e.IdComision == id);
        }
    }
}
