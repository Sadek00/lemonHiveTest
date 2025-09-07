using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalAppointmentSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "CreatedDate", "DoctorEmail", "DoctorName", "DoctorPhone", "Specialization", "UpdatedDate" },
                values: new object[,]
                {
                    { "1", null, null, "Dr. John Smith", null, "Cardiologist", null },
                    { "2", null, null, "Dr. Emily Brown", null, "Dermatologist", null },
                    { "3", null, null, "Dr. Bell Smith", null, "Cardiologist", null },
                    { "4", null, null, "Dr. Hars Brown", null, "Dermatologist", null }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "CreatedDate", "PatientAddress", "PatientEmail", "PatientName", "PatientPhone", "UpdatedDate" },
                values: new object[,]
                {
                    { "1", null, null, "Sadek.gtrbd@gmail.com", "Alice Johnson", null, null },
                    { "2", null, null, "Sadek.gtrbd@gmail.com", "Emile Johnson", null, null },
                    { "3", null, null, "Sadek.gtrbd@gmail.com", "Sarah Davis", null, null },
                    { "4", null, null, "Sadek.gtrbd@gmail.com", "John Doe", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "4");
        }
    }
}
