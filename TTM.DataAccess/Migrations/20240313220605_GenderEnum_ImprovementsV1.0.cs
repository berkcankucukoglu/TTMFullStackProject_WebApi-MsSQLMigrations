using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTM.DataAccess.Migrations
{
    public partial class GenderEnum_ImprovementsV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Gender",
                table: "User",
                type: "smallint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gender",
                value: (short)0);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "Gender", "LastName", "Password" },
                values: new object[] { 2, "janedoe@mail.com", "Jane", (short)2, "Doe", "54321" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");
        }
    }
}
