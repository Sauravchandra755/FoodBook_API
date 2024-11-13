using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FB__Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MId = table.Column<string>(type: "varchar(50)", nullable: false),
                    EmployeeName = table.Column<string>(type: "varchar(50)", nullable: false),
                    DailyCredits = table.Column<decimal>(type: "money", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FB__Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FB__Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "varchar(50)", nullable: false),
                    FoodDescription = table.Column<string>(type: "varchar(200)", nullable: false),
                    FoodCatagory = table.Column<string>(type: "varchar(50)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FB__Foods", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "FB__Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    //EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FB__Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_FB__Orders_FB__Employees_EmployeeId",
                        column: x => x.Id,
                        principalTable: "FB__Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FB__Orders_FB__Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "FB__Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FB__Orders_EmployeeId",
                table: "FB__Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FB__Orders_FoodId",
                table: "FB__Orders",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FB__Orders");

            migrationBuilder.DropTable(
                name: "FB__Employees");

            migrationBuilder.DropTable(
                name: "FB__Foods");
        }
    }
}
