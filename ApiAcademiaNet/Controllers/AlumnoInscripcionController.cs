using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAcademiaNet.Models;
using ApiAcademiaNet.Data;

namespace ApiAcademiaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoInscripcionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlumnoInscripcionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AlumnoInscripcion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoInscripcion>>> GetAlumnoInscripciones()
        {
            return await _context.AlumnoInscripcion
                .Include(ai => ai.Persona)
                .Include(ai => ai.Curso)
                .ToListAsync();
        }

        // GET: api/AlumnoInscripcion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlumnoInscripcion>> GetAlumnoInscripcion(int id)
        {
            var alumnoInscripcion = await _context.AlumnoInscripcion
                .Include(ai => ai.Persona)
                .Include(ai => ai.Curso)
                .FirstOrDefaultAsync(ai => ai.IdInscripcion == id);

            if (alumnoInscripcion == null)
            {
                return NotFound();
            }

            return alumnoInscripcion;
        }

        // POST: api/AlumnoInscripcion
        [HttpPost]
        public async Task<ActionResult<AlumnoInscripcion>> PostAlumnoInscripcion(AlumnoInscripcion alumnoInscripcion)
        {
            // Validar existencia de Persona y Curso
            var personaExists = await _context.Persona.AnyAsync(p => p.IdPersona == alumnoInscripcion.IdPersona);
            if (!personaExists)
            {
                return BadRequest("La Persona especificada no existe.");
            }

            var cursoExists = await _context.Curso.AnyAsync(c => c.IdCurso == alumnoInscripcion.IdCurso);
            if (!cursoExists)
            {
                return BadRequest("El Curso especificado no existe.");
            }

            // Validar que la inscripción no exista ya para la misma persona y curso
            var inscripcionExists = await _context.AlumnoInscripcion.AnyAsync(ai =>
                ai.IdPersona == alumnoInscripcion.IdPersona && ai.IdCurso == alumnoInscripcion.IdCurso);

            if (inscripcionExists)
            {
                return Conflict("Ya existe una inscripción para esta persona y este curso.");
            }

            _context.AlumnoInscripcion.Add(alumnoInscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumnoInscripcion), new { id = alumnoInscripcion.IdInscripcion }, alumnoInscripcion);
        }


        // PUT: api/AlumnoInscripcion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumnoInscripcion(int id, AlumnoInscripcion alumnoInscripcion)
        {
            if (id != alumnoInscripcion.IdInscripcion)
            {
                return BadRequest();
            }

            // Validar existencia de Persona y Curso
            var personaExists = await _context.Persona.AnyAsync(p => p.IdPersona == alumnoInscripcion.IdPersona);
            if (!personaExists)
            {
                return BadRequest("La Persona especificada no existe.");
            }

            var cursoExists = await _context.Curso.AnyAsync(c => c.IdCurso == alumnoInscripcion.IdCurso);
            if (!cursoExists)
            {
                return BadRequest("El Curso especificado no existe.");
            }

            _context.Entry(alumnoInscripcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoInscripcionExists(id))
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

        // DELETE: api/AlumnoInscripcion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumnoInscripcion(int id)
        {
            var alumnoInscripcion = await _context.AlumnoInscripcion.FindAsync(id);
            if (alumnoInscripcion == null)
            {
                return NotFound();
            }

            _context.AlumnoInscripcion.Remove(alumnoInscripcion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/AlumnoInscripcion/Persona/5
        [HttpGet("Persona/{idPersona}")]
        public async Task<ActionResult<IEnumerable<AlumnoInscripcion>>> GetInscripcionesPorPersona(int idPersona)
        {
            var inscripciones = await _context.AlumnoInscripcion
                .Include(ai => ai.Persona)
                .Include(ai => ai.Curso)
                .Where(ai => ai.IdPersona == idPersona)
                .ToListAsync();

            if (inscripciones == null || !inscripciones.Any())
            {
                return NotFound($"No se encontraron inscripciones para la persona con ID {idPersona}.");
            }

            return Ok(inscripciones);
        }

        // GET: api/AlumnoInscripcion/Curso/5
        [HttpGet("Cursos/{idCurso}")]
        public async Task<ActionResult<IEnumerable<AlumnoInscripcion>>> GetInscripcionesPorCurso(int idCurso)
        {
            var inscripciones = await _context.AlumnoInscripcion
                .Include(ai => ai.Persona)
                .Include(ai => ai.Curso)
                .Where(ai => ai.IdCurso == idCurso)
                .ToListAsync();

            if (inscripciones == null || !inscripciones.Any())
            {
                return NotFound($"No se encontraron inscripciones para la persona con ID {idCurso}.");
            }

            return Ok(inscripciones);
        }

        // GET: api/AlumnoInscripcion/CursosArray
        [HttpPost("CursosArray")]
        public async Task<ActionResult<IEnumerable<AlumnoInscripcion>>> GetInscripcionesPorCursos([FromBody] int[] idCursos)
        {
            if (idCursos == null || idCursos.Length == 0)
            {
                return BadRequest("Debe proporcionar un array de IDs de cursos.");
            }

            var inscripciones = await _context.AlumnoInscripcion
                .Include(ai => ai.Persona)
                .Include(ai => ai.Curso)
                .Where(ai => idCursos.Contains(ai.IdCurso))
                .ToListAsync();

            if (inscripciones == null || !inscripciones.Any())
            {
                return NotFound("No se encontraron inscripciones para los cursos especificados.");
            }

            return Ok(inscripciones);
        }

        private bool AlumnoInscripcionExists(int id)
        {
            return _context.AlumnoInscripcion.Any(e => e.IdInscripcion == id);
        }
    }
}
