using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service_Container.Migrations
{
    public partial class BlogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogsSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TravelPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdaetDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogsImageSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BlogsSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsImageSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogsImageSections_BlogsSections_BlogsSectionId",
                        column: x => x.BlogsSectionId,
                        principalTable: "BlogsSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogsImageSections_BlogsSectionId",
                table: "BlogsImageSections",
                column: "BlogsSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogsImageSections");

            migrationBuilder.DropTable(
                name: "BlogsSections");
        }
    }
}
