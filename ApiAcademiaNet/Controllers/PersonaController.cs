using ApiAcademiaNet.Data;
using ApiAcademiaNet.Models;
using ApiAcademiaNet.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiAcademiaNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _secretKey;

        public PersonaController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _secretKey = configuration["Jwt:Key"];
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Clave));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                loginDto.Clave = builder.ToString();
            }

            var persona = await _context.Persona
                .SingleOrDefaultAsync(p => p.Legajo == loginDto.Legajo && p.Clave == loginDto.Clave);

            if (persona == null)
            {
                return Unauthorized(new ResponseDto { IsSuccess = false, Message = "Credenciales inválidas" });
            }

            var token = GenerateJwtToken(persona);

            return Ok(new ResponseDto
            {
                Data = new { Token = token, IdPersona = persona.IdPersona, Tipo = persona.Tipo },
                IsSuccess = true,
                Message = "Inicio de sesión exitoso"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonas()
        {
            var personas = await _context.Persona.ToListAsync();
            return Ok(new ResponseDto { Data = personas });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Persona no encontrada" });
            }

            return Ok(new ResponseDto { Data = persona });
        }


        // POST: api/Persona
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostPersona([FromBody] Persona persona)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                // Validar que el campo IdPlan esté presente para los alumnos
                if (persona.Tipo == "Alumno" && !persona.IdPlan.HasValue)
                {
                    response.IsSuccess = false;
                    response.Message = "El campo IdPlan es requerido para los alumnos.";
                    return BadRequest(response);
                }

                // Para tipos que no son "Alumno", asegurarse de que IdPlan sea null
                if (persona.Tipo != "Alumno")
                {
                    persona.IdPlan = null;
                }

                // Si IdPlan es proporcionado, verificar que el Plan existe
                if (persona.IdPlan.HasValue)
                {
                    var plan = await _context.Plan.FindAsync(persona.IdPlan.Value);
                    if (plan == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "El Plan especificado no existe.";
                        return BadRequest(response);
                    }
                }

                // Hash de la clave utilizando SHA-256
                if (!string.IsNullOrEmpty(persona.Clave))
                {
                    persona.Clave = HashPassword(persona.Clave);
                }

                // Agregar la nueva persona a la base de datos
                _context.Persona.Add(persona);
                await _context.SaveChangesAsync();

                response.Data = persona;
                response.Message = "Persona registrada exitosamente.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                response.IsSuccess = false;
                response.Message = $"Error al registrar la persona: {ex.Message}";
                return StatusCode(500, response);
            }
        }
        // Método para realizar el hashing de la clave
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // PUT: api/Persona/5
        [HttpPut("{id}")]
            public async Task<ActionResult<ResponseDto>> PutPersona(int id, Persona persona)
            {
                ResponseDto response = new ResponseDto();

                if (id != persona.IdPersona)
                {
                    response.IsSuccess = false;
                    response.Message = "El ID proporcionado no coincide con el de la persona.";
                    return BadRequest(response);
                }

                try
                {
                    if (persona.Tipo == "Alumno" && persona.IdPlan == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "El campo IdPlan es requerido para los alumnos.";
                        return BadRequest(response);
                    }

                    if (persona.Tipo != "Alumno")
                    {
                        persona.IdPlan = null; // Asegurar que IdPlan sea null para Docente y NoDocente
                    }

                    _context.Entry(persona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();

                    response.Data = persona;
                    response.Message = "Persona actualizada exitosamente.";
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    if (!PersonaExists(id))
                    {
                        response.IsSuccess = false;
                        response.Message = "Persona no encontrada.";
                        return NotFound(response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"Error al actualizar la persona: {ex.Message}";
                        return StatusCode(500, response);
                    }
                }
            }

            private bool PersonaExists(int id)
            {
                return _context.Persona.Any(e => e.IdPersona == id);
            }
        
    
       [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound(new ResponseDto { IsSuccess = false, Message = "Persona no encontrada" });
            }

            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto { IsSuccess = true, Message = "Persona eliminada exitosamente" });
        }

        private string GenerateJwtToken(Persona persona)
        {
            // Crear los claims que van dentro del token
            var claims = new[]
            {
           new Claim(JwtRegisteredClaimNames.Sub, persona.IdPersona.ToString()),
           new Claim("tipo", persona.Tipo), // Tipo de usuario (Alumno, Docente, NoDocente)
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crear las credenciales de firma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: "tu-app",
                audience: "tu-app",
                claims: claims,
                expires: DateTime.Now.AddHours(2), // Duración del token
                signingCredentials: creds
            );

            // Serializar el token a una cadena
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
