using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDO.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskImportances",
                columns: table => new
                {
                    taskImportanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskImportanceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskImportances", x => x.taskImportanceId);
                });

            migrationBuilder.CreateTable(
                name: "ToDoTaskStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoTaskStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taskTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    taskCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taskDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    taskEstimate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taskImportanceId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskImportances_taskImportanceId",
                        column: x => x.taskImportanceId,
                        principalTable: "TaskImportances",
                        principalColumn: "taskImportanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_ToDoTaskStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ToDoTaskStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskImportances",
                columns: new[] { "taskImportanceId", "taskImportanceName" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                table: "ToDoTaskStatuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "To Do" },
                    { 2, "Doing" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "3afa697b-d5c1-4ffe-bf00-450971255000", "abdallahkorhani1@gmail.com", true, false, null, "ABDALLAHKORHANI1@GMAIL.COM", "ABDALLAH", "AQAAAAEAACcQAAAAEDzxXzoVO9oaE5s0HlaEdy09lfzkFPfsePdgOR6BNk3dNl0yG7Odc28Ml9QXkZlZAQ==", null, false, "36c3f4c8-88ab-421e-a892-d5278dba4935", false, "abdallah" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "StatusId", "taskCategory", "taskDueDate", "taskEstimate", "taskImportanceId", "taskTitle", "userId" },
                values: new object[,]
                {
                    { 1, 1, "Fixing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 hours", 1, "Fixing Laptop", 1 },
                    { 2, 2, "job opportunity", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 hours", 3, "Assessment", 1 },
                    { 3, 3, "senior", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 hour", 2, "Senior project", 1 },
                    { 4, 1, "Sport", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 hours", 3, "Football", 1 },
                    { 5, 1, "Reading", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 hour", 1, "Reading a book", 1 },
                    { 6, 1, "idk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 hour", 3, "Vsisiting grandma", 1 },
                    { 7, 1, "Fixing", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 hour", 1, "Fixing TV", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_taskImportanceId",
                table: "Tasks",
                column: "taskImportanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_userId",
                table: "Tasks",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskImportances");

            migrationBuilder.DropTable(
                name: "ToDoTaskStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
