using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class change_user_req : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRequest_Users_UserId",
                table: "UserRequest");

            migrationBuilder.DropIndex(
                name: "IX_UserRequest_UserId",
                table: "UserRequest");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequest_RequestedUserId",
                table: "UserRequest",
                column: "RequestedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequest_Users_RequestedUserId",
                table: "UserRequest",
                column: "RequestedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRequest_Users_RequestedUserId",
                table: "UserRequest");

            migrationBuilder.DropIndex(
                name: "IX_UserRequest_RequestedUserId",
                table: "UserRequest");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserRequest_UserId",
                table: "UserRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequest_Users_UserId",
                table: "UserRequest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
