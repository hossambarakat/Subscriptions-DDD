using Microsoft.EntityFrameworkCore.Migrations;

namespace Subscriptions.Before.Migrations
{
    public partial class AddMoneySpent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MoneySpent",
                table: "Customer",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneySpent",
                table: "Customer");
        }
    }
}
