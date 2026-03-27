using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamenUnidadDos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    documento = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    fecha_contratacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    departamento = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    puesto_trabajo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    salario_base = table.Column<decimal>(type: "TEXT", nullable: false),
                    activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planillas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    periodo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fecha_pago = table.Column<DateTime>(type: "TEXT", nullable: false),
                    estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planillas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPlanilla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    planilla_id = table.Column<int>(type: "INTEGER", nullable: false),
                    empleado_id = table.Column<int>(type: "INTEGER", nullable: false),
                    salario_base = table.Column<decimal>(type: "TEXT", nullable: false),
                    horas_extra = table.Column<decimal>(type: "TEXT", nullable: false),
                    monto_horas_extra = table.Column<decimal>(type: "TEXT", nullable: false),
                    bonificaciones = table.Column<decimal>(type: "TEXT", nullable: false),
                    deducciones = table.Column<decimal>(type: "TEXT", nullable: false),
                    salario_neto = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPlanilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesPlanilla_Empleados_empleado_id",
                        column: x => x.empleado_id,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPlanilla_Planillas_planilla_id",
                        column: x => x.planilla_id,
                        principalTable: "Planillas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPlanilla_empleado_id",
                table: "DetallesPlanilla",
                column: "empleado_id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPlanilla_planilla_id",
                table: "DetallesPlanilla",
                column: "planilla_id");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_documento",
                table: "Empleados",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_periodo",
                table: "Planillas",
                column: "periodo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPlanilla");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Planillas");
        }
    }
}
