using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addPortfolioTableNameInData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b640957-bbbb-4981-93a5-6156e61c41f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "891d8826-5852-4dd7-8e32-058eb07b4452");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b34e23ec-cdbc-4ad4-a1a1-f8369f516e2b", null, "Admin", "ADMIN" },
                    { "ec3e1e6f-3b2a-4dd4-bcc9-a59e918a73a7", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b34e23ec-cdbc-4ad4-a1a1-f8369f516e2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec3e1e6f-3b2a-4dd4-bcc9-a59e918a73a7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b640957-bbbb-4981-93a5-6156e61c41f3", null, "Admin", "ADMIN" },
                    { "891d8826-5852-4dd7-8e32-058eb07b4452", null, "User", "USER" }
                });
        }
    }
}
