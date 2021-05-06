using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace booka.counter.Migrations
{
    public partial class timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Counts_AspNetUsers_UserId",
                table: "Counts");

            migrationBuilder.DropForeignKey(
                name: "FK_Counts_Tags_TagId",
                table: "Counts");

            migrationBuilder.DropIndex(
                name: "IX_Counts_TagId",
                table: "Counts");

            migrationBuilder.DropIndex(
                name: "IX_Counts_UserId",
                table: "Counts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Counts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "Counts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Counts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Counts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Counts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TagId",
                table: "Counts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Counts_TagId",
                table: "Counts",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Counts_UserId",
                table: "Counts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Counts_AspNetUsers_UserId",
                table: "Counts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Counts_Tags_TagId",
                table: "Counts",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
