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
    public class MateriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MateriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Materia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMaterias()
        {
            return await _context.Materia.Include(m => m.Plan).ToListAsync();
        }

        // GET: api/Materia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMateria(int id)
        {
            var materia = await _context.Materia.Include(m => m.Plan)
                                                .FirstOrDefaultAsync(m => m.IdMateria == id);

            if (materia == null)
            {
                return NotFound();
            }

            return materia;
        }

        // POST: api/Materia
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMateria(Materia materia)
        {
            // Check if Plan exists
            var planExists = await _context.Plan.AnyAsync(p => p.IdPlan == materia.IdPlan);
            if (!planExists)
            {
                return BadRequest("El Plan especificado no existe.");
            }

            _context.Materia.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMateria), new { id = materia.IdMateria }, materia);
        }

        // PUT: api/Materia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, Materia materia)
        {
            if (id != materia.IdMateria)
            {
                return BadRequest();
            }

            // Check if Plan exists
            var planExists = await _context.Plan.AnyAsync(p => p.IdPlan == materia.IdPlan);
            if (!planExists)
            {
                return BadRequest("El Plan especificado no existe.");
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(id))
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

        // DELETE: api/Materia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var materia = await _context.Materia.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            _context.Materia.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaExists(int id)
        {
            return _context.Materia.Any(e => e.IdMateria == id);
        }
    }
}
