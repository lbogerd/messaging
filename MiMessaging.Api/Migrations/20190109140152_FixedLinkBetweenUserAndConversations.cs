using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiMessaging.Api.Migrations
{
    public partial class FixedLinkBetweenUserAndConversations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Conversations_ConversationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ConversationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConversationId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConversationId",
                table: "Users",
                column: "ConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Conversations_ConversationId",
                table: "Users",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
