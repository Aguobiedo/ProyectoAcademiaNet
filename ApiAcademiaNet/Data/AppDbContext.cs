using Microsoft.EntityFrameworkCore;
using ApiAcademiaNet.Models;

namespace ApiAcademiaNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<AlumnoInscripcion> AlumnoInscripcion { get; set; }
        public DbSet<DocenteCurso> DocenteCurso { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<Comision> Comision { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             

            // Configuración de las relaciones
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Materia)
                .WithMany(m => m.Curso)
                .HasForeignKey(c => c.IdMateria)
                .OnDelete(DeleteBehavior.NoAction);  // Sin eliminación en cascada

            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Comision)
                .WithMany(co => co.Curso)
                .HasForeignKey(c => c.IdComision)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AlumnoInscripcion>()
                .HasOne(ai => ai.Persona)
                .WithMany(p => p.AlumnoInscripcion)
                .HasForeignKey(ai => ai.IdPersona)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AlumnoInscripcion>()
                .HasOne(ai => ai.Curso)
                .WithMany(c => c.AlumnoInscripcion)
                .HasForeignKey(ai => ai.IdCurso)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocenteCurso>()
                .HasOne(dc => dc.Curso)
                .WithMany(c => c.DocenteCurso)
                .HasForeignKey(dc => dc.IdCurso)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocenteCurso>()
                .HasOne(dc => dc.Persona)
                .WithMany(p => p.DocenteCurso)
                .HasForeignKey(dc => dc.IdPersona)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Materia>()
                .HasOne(c => c.Plan)
                .WithMany(m => m.Materia)
                .HasForeignKey(c => c.IdPlan)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comision>()
                .HasOne(c => c.Plan)
                .WithMany(m => m.Comision)
                .HasForeignKey(c => c.IdPlan)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Plan>()
                .HasOne(c => c.Especialidad)
                .WithMany(m => m.Plan)
                .HasForeignKey(c => c.IdEspecialidad)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Persona>()
                .HasOne(c => c.Plan)
                .WithMany(m => m.Persona)
                .HasForeignKey(c => c.IdPlan)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
