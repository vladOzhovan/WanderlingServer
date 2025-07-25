using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakePlantEffectOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantEffect_Plants_PlantEntityId",
                table: "PlantEffect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantEffect",
                table: "PlantEffect");

            migrationBuilder.RenameColumn(
                name: "PlantEntityId",
                table: "PlantEffect",
                newName: "PlantId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PlantEffect",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantEffect",
                table: "PlantEffect",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlantEffect_PlantId",
                table: "PlantEffect",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantEffect_Plants_PlantId",
                table: "PlantEffect",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantEffect_Plants_PlantId",
                table: "PlantEffect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantEffect",
                table: "PlantEffect");

            migrationBuilder.DropIndex(
                name: "IX_PlantEffect_PlantId",
                table: "PlantEffect");

            migrationBuilder.RenameColumn(
                name: "PlantId",
                table: "PlantEffect",
                newName: "PlantEntityId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PlantEffect",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantEffect",
                table: "PlantEffect",
                columns: new[] { "PlantEntityId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlantEffect_Plants_PlantEntityId",
                table: "PlantEffect",
                column: "PlantEntityId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
