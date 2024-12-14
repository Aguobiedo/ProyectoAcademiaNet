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
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Curso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            return await _context.Curso
                                 .Include(c => c.Materia)
                                 .Include(c => c.Comision)
                                 .ToListAsync();
        }

        // GET: api/Curso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Curso
                                      .Include(c => c.Materia)
                                      .Include(c => c.Comision)
                                      .FirstOrDefaultAsync(c => c.IdCurso == id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // POST: api/Curso
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            // Verificar que la Materia existe
            var materiaExists = await _context.Materia.AnyAsync(m => m.IdMateria == curso.IdMateria);
            if (!materiaExists)
            {
                return BadRequest("La Materia especificada no existe.");
            }

            // Verificar que la Comision existe
            var comisionExists = await _context.Comision.AnyAsync(c => c.IdComision == curso.IdComision);
            if (!comisionExists)
            {
                return BadRequest("La Comision especificada no existe.");
            }

            _context.Curso.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.IdCurso }, curso);
        }

        // PUT: api/Curso/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            if (id != curso.IdCurso)
            {
                return BadRequest();
            }

            // Verificar que la Materia existe
            var materiaExists = await _context.Materia.AnyAsync(m => m.IdMateria == curso.IdMateria);
            if (!materiaExists)
            {
                return BadRequest("La Materia especificada no existe.");
            }

            // Verificar que la Comision existe
            var comisionExists = await _context.Comision.AnyAsync(c => c.IdComision == curso.IdComision);
            if (!comisionExists)
            {
                return BadRequest("La Comision especificada no existe.");
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // DELETE: api/Curso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Curso/plan/5
        [HttpGet("plan/{id}")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorIdPlan(int id)
        {
            var cursos = await _context.Curso
                                       .Include(c => c.Materia)
                                       .Include(c => c.Comision)
                                       .Where(c => c.Materia != null && c.Materia.IdPlan == id) // Filtrar por IdPlan a través de Materia
                                       .ToListAsync();

            if (cursos == null || cursos.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron cursos para el IdPlan especificado" });
            }

            return Ok(cursos);
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.IdCurso == id);
        }
    }
}
