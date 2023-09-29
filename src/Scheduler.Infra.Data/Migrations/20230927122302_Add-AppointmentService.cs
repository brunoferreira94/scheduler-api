using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentService",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentService", x => new { x.AppointmentId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_AppointmentService_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("18991fb6-827d-4a07-8a8c-33cc7f38dba4"), null, "Luzes" },
                    { new Guid("820d24da-2abf-4661-97a8-529ce54cc378"), null, "Corte" },
                    { new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812"), null, "Barba" }
                });

            migrationBuilder.InsertData(
                table: "AppointmentService",
                columns: new[] { "AppointmentId", "ServiceId" },
                values: new object[,]
                {
                    { new Guid("4e7fe69c-7e2e-4fb8-bc1a-4fe91b38d597"), new Guid("18991fb6-827d-4a07-8a8c-33cc7f38dba4") },
                    { new Guid("4e7fe69c-7e2e-4fb8-bc1a-4fe91b38d597"), new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812") },
                    { new Guid("a5a8621f-0930-4199-b0a8-e43ca5d89b74"), new Guid("820d24da-2abf-4661-97a8-529ce54cc378") },
                    { new Guid("a5a8621f-0930-4199-b0a8-e43ca5d89b74"), new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812") },
                    { new Guid("f3071fb3-aafc-4f06-9107-b29281354266"), new Guid("820d24da-2abf-4661-97a8-529ce54cc378") },
                    { new Guid("f3071fb3-aafc-4f06-9107-b29281354266"), new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Email",
                table: "Client",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Name",
                table: "Service",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentService");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Client_Email",
                table: "Client");
        }
    }
}
