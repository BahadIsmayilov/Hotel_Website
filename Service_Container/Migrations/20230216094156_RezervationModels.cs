using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service_Container.Migrations
{
    public partial class RezervationModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    PayFifty = table.Column<bool>(type: "bit", nullable: true),
                    PayAll = table.Column<bool>(type: "bit", nullable: true),
                    Roomtype = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdultsCount = table.Column<int>(type: "int", nullable: false),
                    ChildCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeRoomCategorySections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeRoomCategorySections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeRoomSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    BedType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeRoomSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeRoomSections_HomeRoomCategorySections_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "HomeRoomCategorySections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeImageRoomSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HomeRoomSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeImageRoomSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeImageRoomSections_HomeRoomSections_HomeRoomSectionId",
                        column: x => x.HomeRoomSectionId,
                        principalTable: "HomeRoomSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    HomeRoomSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_HomeRoomSections_HomeRoomSectionId",
                        column: x => x.HomeRoomSectionId,
                        principalTable: "HomeRoomSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HomeRoomSectionId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOrderStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomOrderStatuses_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomOrderStatuses_HomeRoomSections_HomeRoomSectionId",
                        column: x => x.HomeRoomSectionId,
                        principalTable: "HomeRoomSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeImageRoomSections_HomeRoomSectionId",
                table: "HomeImageRoomSections",
                column: "HomeRoomSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeRoomSections_CategoryId",
                table: "HomeRoomSections",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_HomeRoomSectionId",
                table: "Payments",
                column: "HomeRoomSectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomOrderStatuses_BookingId",
                table: "RoomOrderStatuses",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomOrderStatuses_HomeRoomSectionId",
                table: "RoomOrderStatuses",
                column: "HomeRoomSectionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeImageRoomSections");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RoomOrderStatuses");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "HomeRoomSections");

            migrationBuilder.DropTable(
                name: "HomeRoomCategorySections");
        }
    }
}
