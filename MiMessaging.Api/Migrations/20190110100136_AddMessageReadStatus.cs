using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiMessaging.Api.Migrations
{
    public partial class AddMessageReadStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageReadStatuses",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(nullable: false),
                    ParticipantConversationId = table.Column<Guid>(nullable: true),
                    ParticipantId1 = table.Column<Guid>(nullable: true),
                    MessageId = table.Column<Guid>(nullable: false),
                    ReceivedOn = table.Column<DateTime>(nullable: false),
                    ReadOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReadStatuses", x => new { x.ParticipantId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_MessageReadStatuses_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReadStatuses_ConversationParticipants_ParticipantConversationId_ParticipantId1",
                        columns: x => new { x.ParticipantConversationId, x.ParticipantId1 },
                        principalTable: "ConversationParticipants",
                        principalColumns: new[] { "ConversationId", "ParticipantId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadStatuses_MessageId",
                table: "MessageReadStatuses",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReadStatuses_ParticipantConversationId_ParticipantId1",
                table: "MessageReadStatuses",
                columns: new[] { "ParticipantConversationId", "ParticipantId1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReadStatuses");
        }
    }
}
