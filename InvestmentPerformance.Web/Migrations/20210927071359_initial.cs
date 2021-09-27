using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvestmentPerformance.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    InvestmentType = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdateDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdateDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInvestments",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvestmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CostBasis = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Gain = table.Column<decimal>(type: "TEXT", nullable: false),
                    InvestmentStatus = table.Column<string>(type: "TEXT", nullable: false),
                    InvestmentTerm = table.Column<string>(type: "TEXT", nullable: false),
                    AcquireDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    SellDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastUpdateDateUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInvestments", x => new { x.UserId, x.InvestmentId });
                    table.ForeignKey(
                        name: "FK_UserInvestments_Investment_InvestmentId",
                        column: x => x.InvestmentId,
                        principalTable: "Investment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInvestments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Investment",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "InvestmentType", "LastUpdateDateUtc", "LastUpdatedBy", "Name" },
                values: new object[] { 1, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 467, DateTimeKind.Unspecified).AddTicks(8970), new TimeSpan(0, 0, 0, 0, 0)), "Stock", new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 467, DateTimeKind.Unspecified).AddTicks(9020), new TimeSpan(0, 0, 0, 0, 0)), null, "AAPL" });

            migrationBuilder.InsertData(
                table: "Investment",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "InvestmentType", "LastUpdateDateUtc", "LastUpdatedBy", "Name" },
                values: new object[] { 2, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 468, DateTimeKind.Unspecified).AddTicks(870), new TimeSpan(0, 0, 0, 0, 0)), "Stock", new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 468, DateTimeKind.Unspecified).AddTicks(920), new TimeSpan(0, 0, 0, 0, 0)), null, "MSFT" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "LastUpdateDateUtc", "LastUpdatedBy", "Name" },
                values: new object[] { 1, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 467, DateTimeKind.Unspecified).AddTicks(5970), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 467, DateTimeKind.Unspecified).AddTicks(6140), new TimeSpan(0, 0, 0, 0, 0)), null, "Ari" });

            migrationBuilder.InsertData(
                table: "UserInvestments",
                columns: new[] { "InvestmentId", "UserId", "AcquireDateUtc", "CostBasis", "CreatedBy", "CreatedDateUtc", "CurrentPrice", "CurrentValue", "Gain", "Id", "InvestmentStatus", "InvestmentTerm", "LastUpdateDateUtc", "LastUpdatedBy", "Quantity", "SellDateUtc" },
                values: new object[] { 1, 1, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 470, DateTimeKind.Unspecified).AddTicks(7950), new TimeSpan(0, 0, 0, 0, 0)), 20m, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 470, DateTimeKind.Unspecified).AddTicks(8220), new TimeSpan(0, 0, 0, 0, 0)), 0m, 0m, 0m, 1, "Active", "ShortTerm", new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 470, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)), null, 2m, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "UserInvestments",
                columns: new[] { "InvestmentId", "UserId", "AcquireDateUtc", "CostBasis", "CreatedBy", "CreatedDateUtc", "CurrentPrice", "CurrentValue", "Gain", "Id", "InvestmentStatus", "InvestmentTerm", "LastUpdateDateUtc", "LastUpdatedBy", "Quantity", "SellDateUtc" },
                values: new object[] { 2, 1, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 472, DateTimeKind.Unspecified).AddTicks(1460), new TimeSpan(0, 0, 0, 0, 0)), 20m, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 471, DateTimeKind.Unspecified).AddTicks(6470), new TimeSpan(0, 0, 0, 0, 0)), 30m, 60m, 20m, 2, "Inactive", "ShortTerm", new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 471, DateTimeKind.Unspecified).AddTicks(6470), new TimeSpan(0, 0, 0, 0, 0)), null, 2m, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 13, 58, 472, DateTimeKind.Unspecified).AddTicks(2790), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Investment_Id",
                table: "Investment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Investment_Name",
                table: "Investment",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvestments_Id",
                table: "UserInvestments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInvestments_InvestmentId",
                table: "UserInvestments",
                column: "InvestmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInvestments");

            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
