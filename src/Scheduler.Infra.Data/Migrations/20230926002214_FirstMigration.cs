using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Email", "ModifiedDate", "Name" },
                values: new object[] { new Guid("5748500f-98f9-460b-8aaf-a39591065aec"), "email@email.com", null, "João da Silva" });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "ClientId", "ModifiedDate", "ScheduledDate" },
                values: new object[,]
                {
                    { new Guid("4e7fe69c-7e2e-4fb8-bc1a-4fe91b38d597"), new Guid("5748500f-98f9-460b-8aaf-a39591065aec"), null, new DateTime(2023, 10, 30, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a5a8621f-0930-4199-b0a8-e43ca5d89b74"), new Guid("5748500f-98f9-460b-8aaf-a39591065aec"), null, new DateTime(2023, 10, 15, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f3071fb3-aafc-4f06-9107-b29281354266"), new Guid("5748500f-98f9-460b-8aaf-a39591065aec"), null, new DateTime(2023, 9, 29, 8, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ClientId",
                table: "Appointment",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
