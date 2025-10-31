using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universidad.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    FacultadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Decano = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.FacultadId);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    CarreraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultadId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DuracionSemestres = table.Column<int>(type: "int", nullable: false),
                    TituloOtorgado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.CarreraId);
                    table.ForeignKey(
                        name: "FK_Carreras_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
                        principalColumn: "FacultadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Creditos = table.Column<int>(type: "int", nullable: false),
                    NivelSemestre = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Cursos_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "CarreraId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prerrequisitos",
                columns: table => new
                {
                    PrerrequisitoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    CursoReqId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerrequisitos", x => x.PrerrequisitoId);
                    table.CheckConstraint("CK_Prerrequisito_CursoDiferente", "[CursoId] != [CursoReqId]");
                    table.ForeignKey(
                        name: "FK_Prerrequisitos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerrequisitos_Cursos_CursoReqId",
                        column: x => x.CursoReqId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    SeccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CapacidadMaxima = table.Column<int>(type: "int", nullable: false),
                    Aula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Horario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Dias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PeriodoAcademico = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.SeccionId);
                    table.CheckConstraint("CK_Seccion_CapacidadMaxima", "[CapacidadMaxima] > 0");
                    table.ForeignKey(
                        name: "FK_Secciones_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_FacultadId_Nombre",
                table: "Carreras",
                columns: new[] { "FacultadId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_CarreraId_Nombre",
                table: "Cursos",
                columns: new[] { "CarreraId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_Codigo",
                table: "Cursos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facultades_Nombre",
                table: "Facultades",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_CursoId_CursoReqId",
                table: "Prerrequisitos",
                columns: new[] { "CursoId", "CursoReqId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerrequisitos_CursoReqId",
                table: "Prerrequisitos",
                column: "CursoReqId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_CursoId_Codigo_PeriodoAcademico",
                table: "Secciones",
                columns: new[] { "CursoId", "Codigo", "PeriodoAcademico" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prerrequisitos");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Facultades");
        }
    }
}
