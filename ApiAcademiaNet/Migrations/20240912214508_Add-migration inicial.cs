using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAcademiaNet.Migrations
{
    /// <inheritdoc />
    public partial class Addmigrationinicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    IdEspecialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescEspecialidad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.IdEspecialidad);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    IdPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescPlan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdEspecialidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.IdPlan);
                    table.ForeignKey(
                        name: "FK_Plan_Especialidad_IdEspecialidad",
                        column: x => x.IdEspecialidad,
                        principalTable: "Especialidad",
                        principalColumn: "IdEspecialidad");
                });

            migrationBuilder.CreateTable(
                name: "Comision",
                columns: table => new
                {
                    IdComision = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescComision = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdPlan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comision", x => x.IdComision);
                    table.ForeignKey(
                        name: "FK_Comision_Plan_IdPlan",
                        column: x => x.IdPlan,
                        principalTable: "Plan",
                        principalColumn: "IdPlan");
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescMateria = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HsSemanales = table.Column<int>(type: "int", nullable: false),
                    HsTotales = table.Column<int>(type: "int", nullable: false),
                    IdPlan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.IdMateria);
                    table.ForeignKey(
                        name: "FK_Materia_Plan_IdPlan",
                        column: x => x.IdPlan,
                        principalTable: "Plan",
                        principalColumn: "IdPlan");
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IdPlan = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Persona_Plan_IdPlan",
                        column: x => x.IdPlan,
                        principalTable: "Plan",
                        principalColumn: "IdPlan");
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    IdComision = table.Column<int>(type: "int", nullable: false),
                    AnioCalendario = table.Column<int>(type: "int", nullable: false),
                    cupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.IdCurso);
                    table.ForeignKey(
                        name: "FK_Curso_Comision_IdComision",
                        column: x => x.IdComision,
                        principalTable: "Comision",
                        principalColumn: "IdComision");
                    table.ForeignKey(
                        name: "FK_Curso_Materia_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materia",
                        principalColumn: "IdMateria");
                });

            migrationBuilder.CreateTable(
                name: "AlumnoInscripcion",
                columns: table => new
                {
                    IdInscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    condicion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nota = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoInscripcion", x => x.IdInscripcion);
                    table.ForeignKey(
                        name: "FK_AlumnoInscripcion_Curso_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Curso",
                        principalColumn: "IdCurso");
                    table.ForeignKey(
                        name: "FK_AlumnoInscripcion_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateTable(
                name: "DocenteCurso",
                columns: table => new
                {
                    IdDictado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocenteCurso", x => x.IdDictado);
                    table.ForeignKey(
                        name: "FK_DocenteCurso_Curso_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Curso",
                        principalColumn: "IdCurso");
                    table.ForeignKey(
                        name: "FK_DocenteCurso_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoInscripcion_IdCurso",
                table: "AlumnoInscripcion",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoInscripcion_IdPersona",
                table: "AlumnoInscripcion",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Comision_IdPlan",
                table: "Comision",
                column: "IdPlan");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_IdComision",
                table: "Curso",
                column: "IdComision");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_IdMateria",
                table: "Curso",
                column: "IdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteCurso_IdCurso",
                table: "DocenteCurso",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_DocenteCurso_IdPersona",
                table: "DocenteCurso",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_IdPlan",
                table: "Materia",
                column: "IdPlan");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdPlan",
                table: "Persona",
                column: "IdPlan");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_IdEspecialidad",
                table: "Plan",
                column: "IdEspecialidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoInscripcion");

            migrationBuilder.DropTable(
                name: "DocenteCurso");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Comision");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "Especialidad");
        }
    }
}
