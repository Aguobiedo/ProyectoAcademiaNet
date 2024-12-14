using ApiAcademiaNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios de Entity Framework Core con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// servicios de controladores
builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI para documentación de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de autenticación con JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]); // Leer clave JWT desde appsettings.json
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Desactivar verificación HTTPS en desarrollo
    options.SaveToken = true; // Guardar el token automáticamente
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // Validar la firma del token
        IssuerSigningKey = new SymmetricSecurityKey(key), // Clave utilizada para firmar el token
        ValidateIssuer = false, // No validar el emisor
        ValidateAudience = false, // No validar la audiencia
        ClockSkew = TimeSpan.Zero // Desactivar tiempo de gracia
    };
});

// Configurar CORS
// Configurar CORS para permitir ambos orígenes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7068", "http://localhost:7068" )  // Asegúrate de permitir ambos orígenes
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configuración de la tubería de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en el entorno de desarrollo para documentación interactiva de la API
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de redirección HTTPS
app.UseHttpsRedirection();

// Habilitar CORS para permitir solicitudes del frontend especificado
app.UseCors("AllowSpecificOrigin");

// Habilitar autenticación (manejo de JWT)
app.UseAuthentication();

// Habilitar autorización (validación de permisos)
app.UseAuthorization();

// Mapeo de controladores (rutas de API)
app.MapControllers();

// Ejecutar la aplicación
app.Run();
