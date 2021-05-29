using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ManyToManyRelationshipChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityAtendee");


            migrationBuilder.CreateTable(
                name: "ActivityAtendees",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtendeeEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityAtendees", x => new { x.AtendeeEmail, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_ActivityAtendees_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityAtendees_Atendees_AtendeeEmail",
                        column: x => x.AtendeeEmail,
                        principalTable: "Atendees",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAtendees_ActivityId",
                table: "ActivityAtendees",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityAtendees");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Activities");

            migrationBuilder.CreateTable(
                name: "ActivityAtendee",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtendeesEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityAtendee", x => new { x.ActivitiesId, x.AtendeesEmail });
                    table.ForeignKey(
                        name: "FK_ActivityAtendee_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityAtendee_Atendees_AtendeesEmail",
                        column: x => x.AtendeesEmail,
                        principalTable: "Atendees",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityAtendee_AtendeesEmail",
                table: "ActivityAtendee",
                column: "AtendeesEmail");
        }
    }
}
