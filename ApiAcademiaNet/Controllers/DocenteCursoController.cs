using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAcademiaNet.Models;
using ApiAcademiaNet.Data;

namespace ApiAcademiaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocenteCursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocenteCursoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DocenteCurso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocenteCurso>>> GetDocenteCursos()
        {
            return await _context.DocenteCurso
                .Include(dc => dc.Curso)
                .Include(dc => dc.Persona)
                .ToListAsync();
        }

                // GET: api/AlumnoInscripcion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocenteCurso>> GetDocenteCurso(int id)
        {
            var docenteCurso = await _context.DocenteCurso
                .Include(ai => ai.Persona)
                .Include(ai => ai.Curso)
                .FirstOrDefaultAsync(ai => ai.IdDictado == id);

            if (docenteCurso == null)
            {
                return NotFound();
            }

            return docenteCurso;
        }


        // POST: api/DocenteCurso
        [HttpPost]
        public async Task<ActionResult<DocenteCurso>> PostDocenteCurso(DocenteCurso docenteCurso)
        {
            // Validar existencia de Curso y Persona
            var cursoExists = await _context.Curso.AnyAsync(c => c.IdCurso == docenteCurso.IdCurso);
            if (!cursoExists)
            {
                return BadRequest("El Curso especificado no existe.");
            }

            var personaExists = await _context.Persona.AnyAsync(p => p.IdPersona == docenteCurso.IdPersona);
            if (!personaExists)
            {
                return BadRequest("La Persona especificada no existe.");
            }

            // Validar que no exista ya una inscripción para el mismo idPersona y idCurso
            var existsInscripcion = await _context.DocenteCurso
                .AnyAsync(dc => dc.IdPersona == docenteCurso.IdPersona && dc.IdCurso == docenteCurso.IdCurso);

            if (existsInscripcion)
            {
                return BadRequest("Ya existe una inscripción para la Persona y el Curso especificados.");
            }

            _context.DocenteCurso.Add(docenteCurso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocenteCurso), new { id = docenteCurso.IdDictado }, docenteCurso);
        }



        // PUT: api/DocenteCurso/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocenteCurso(int id, DocenteCurso docenteCurso)
        {
            if (id != docenteCurso.IdDictado)
            {
                return BadRequest();
            }

            // Validar existencia de Curso y Persona
            var cursoExists = await _context.Curso.AnyAsync(c => c.IdCurso == docenteCurso.IdCurso);
            if (!cursoExists)
            {
                return BadRequest("El Curso especificado no existe.");
            }

            var personaExists = await _context.Persona.AnyAsync(p => p.IdPersona == docenteCurso.IdPersona);
            if (!personaExists)
            {
                return BadRequest("La Persona especificada no existe.");
            }

            _context.Entry(docenteCurso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteCursoExists(id))
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

        // DELETE: api/DocenteCurso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocenteCurso(int id)
        {
            var docenteCurso = await _context.DocenteCurso.FindAsync(id);
            if (docenteCurso == null)
            {
                return NotFound();
            }

            _context.DocenteCurso.Remove(docenteCurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/DocenteCurso/Persona/cursos/{idPersona}
        [HttpGet("Persona/cursos/{idPersona}")]
        public async Task<ActionResult<int[]>> GetCursosPorPersonaArray(int idPersona)
        {
            // Verifica si la persona existe
            var personaExists = await _context.Persona.AnyAsync(p => p.IdPersona == idPersona);
            if (!personaExists)
            {
                return NotFound("La persona especificada no existe.");
            }

            // Obtiene los cursos asociados al idPersona
            var cursosIds = await _context.DocenteCurso
                .Where(dc => dc.IdPersona == idPersona)
                .Select(dc => dc.IdCurso)
                .ToArrayAsync();

            if (cursosIds.Length == 0)
            {
                return NotFound("No se encontraron cursos asociados a la persona especificada.");
            }

            return cursosIds;
        }


        private bool DocenteCursoExists(int id)
        {
            return _context.DocenteCurso.Any(e => e.IdDictado == id);
        }
    }
}
