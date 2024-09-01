using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccsessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BillingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "IncomeAndExpenses",
                newName: "Explanation");

            migrationBuilder.RenameColumn(
                name: "SalesId",
                table: "IncomeAndExpenses",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyUnit",
                table: "IncomeAndExpenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "IncomeAndExpenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyUnit",
                table: "IncomeAndExpenses");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "IncomeAndExpenses");

            migrationBuilder.RenameColumn(
                name: "Explanation",
                table: "IncomeAndExpenses",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "IncomeAndExpenses",
                newName: "SalesId");

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<int>(type: "int", nullable: false),
                    BillingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });
        }
    }
}
