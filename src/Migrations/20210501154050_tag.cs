using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace booka.counter.Migrations
{
    public partial class tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Counts");

            migrationBuilder.AddColumn<Guid>(
                name: "TagId",
                table: "Counts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counts_TagId",
                table: "Counts",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counts_Tags_TagId",
                table: "Counts",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counts_Tags_TagId",
                table: "Counts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Counts_TagId",
                table: "Counts");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Counts");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Counts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
