using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduWork.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedNonWorkingDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NonWorkingDays",
                columns: new[] { "Id", "NonWorkingDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 1), "Nova godina" },
                    { 2, new DateOnly(2024, 6, 1), "Sveta tri kralja" },
                    { 3, new DateOnly(2024, 3, 31), "Uskrs" },
                    { 4, new DateOnly(2024, 4, 1), "Uskrsni ponedjeljak" },
                    { 5, new DateOnly(2024, 4, 17), "Dan izbora" },
                    { 6, new DateOnly(2024, 5, 1), "Praznik rada" },
                    { 7, new DateOnly(2024, 5, 30), "Dan državnosti i Tijelovo" },
                    { 8, new DateOnly(2024, 6, 22), "Dan antifašističke borbe" },
                    { 9, new DateOnly(2024, 8, 5), "Dan pobjede i domovinske zahvalnosti" },
                    { 10, new DateOnly(2024, 8, 15), "Velika Gospa" },
                    { 11, new DateOnly(2024, 11, 1), "Dan svih svetih" },
                    { 12, new DateOnly(2024, 11, 18), "Dan sjećanja na žrtve Domovinskog rata" },
                    { 13, new DateOnly(2024, 12, 25), "Božić" },
                    { 14, new DateOnly(2024, 12, 26), "Sveti Stjepan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "NonWorkingDays",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
