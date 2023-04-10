using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service_Container.Migrations
{
    public partial class AboutUsSecModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutUsSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SmallDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsImageSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AboutUsSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsImageSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutUsImageSections_AboutUsSections_AboutUsSectionId",
                        column: x => x.AboutUsSectionId,
                        principalTable: "AboutUsSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboutUsPreferncesSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prefernce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AboutUsSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUsPreferncesSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutUsPreferncesSections_AboutUsSections_AboutUsSectionId",
                        column: x => x.AboutUsSectionId,
                        principalTable: "AboutUsSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsImageSections_AboutUsSectionId",
                table: "AboutUsImageSections",
                column: "AboutUsSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AboutUsPreferncesSections_AboutUsSectionId",
                table: "AboutUsPreferncesSections",
                column: "AboutUsSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutUsImageSections");

            migrationBuilder.DropTable(
                name: "AboutUsPreferncesSections");

            migrationBuilder.DropTable(
                name: "AboutUsSections");
        }
    }
}
