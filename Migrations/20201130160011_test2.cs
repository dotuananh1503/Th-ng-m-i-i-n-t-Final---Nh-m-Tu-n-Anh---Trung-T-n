using Microsoft.EntityFrameworkCore.Migrations;

namespace Huflix.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1808d16b-774d-4176-8ce3-6221b83d5fa1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a33cd6c6-e59a-45dd-893a-8f5cb361b7a5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab35e982-cc45-481f-a098-a2cb6ac66281", "fb83385e-ee92-4bb9-bab0-0fe25416db29", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c98e4590-0865-4d76-9f31-9173211b771b", "769a6fcd-9a95-4a49-8cf2-c0cd80bb2703", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab35e982-cc45-481f-a098-a2cb6ac66281");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c98e4590-0865-4d76-9f31-9173211b771b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a33cd6c6-e59a-45dd-893a-8f5cb361b7a5", "0100504f-fab1-48e4-8156-820b5eaf9d49", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1808d16b-774d-4176-8ce3-6221b83d5fa1", "5edf5095-b282-4cf0-b43a-6146be16d484", "Administrator", "ADMINISTRATOR" });
        }
    }
}
