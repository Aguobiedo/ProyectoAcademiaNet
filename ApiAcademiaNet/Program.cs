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

// Configuraci�n de Swagger/OpenAPI para documentaci�n de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de autenticaci�n con JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]); // Leer clave JWT desde appsettings.json
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Desactivar verificaci�n HTTPS en desarrollo
    options.SaveToken = true; // Guardar el token autom�ticamente
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
// Configurar CORS para permitir ambos or�genes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7068", "http://localhost:7068" )  // Aseg�rate de permitir ambos or�genes
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configuraci�n de la tuber�a de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en el entorno de desarrollo para documentaci�n interactiva de la API
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuraci�n de redirecci�n HTTPS
app.UseHttpsRedirection();

// Habilitar CORS para permitir solicitudes del frontend especificado
app.UseCors("AllowSpecificOrigin");

// Habilitar autenticaci�n (manejo de JWT)
app.UseAuthentication();

// Habilitar autorizaci�n (validaci�n de permisos)
app.UseAuthorization();

// Mapeo de controladores (rutas de API)
app.MapControllers();

// Ejecutar la aplicaci�n
app.Run();
