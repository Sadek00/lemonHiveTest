using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalAppointmentSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class Addmedicineseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "CreatedDate", "Description", "MedicineName", "UpdatedDate" },
                values: new object[,]
                {
                    { "1", null, "Pain reliever and fever reducer", "Paracetamol", null },
                    { "2", null, "Antibiotic used to treat infections", "Amoxicillin", null },
                    { "3", null, "Anti-inflammatory and pain relief", "Ibuprofen", null },
                    { "4", null, "Antihistamine for allergies", "Cetirizine", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: "4");
        }
    }
}
