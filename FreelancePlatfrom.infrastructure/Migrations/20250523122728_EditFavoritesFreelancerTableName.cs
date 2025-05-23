using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancePlatfrom.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditFavoritesFreelancerTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_ClientId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "FavoritesFreelancer");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_ClientId",
                table: "FavoritesFreelancer",
                newName: "IX_FavoritesFreelancer_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesFreelancer",
                table: "FavoritesFreelancer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesFreelancer_AspNetUsers_ClientId",
                table: "FavoritesFreelancer",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesFreelancer_AspNetUsers_ClientId",
                table: "FavoritesFreelancer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesFreelancer",
                table: "FavoritesFreelancer");

            migrationBuilder.RenameTable(
                name: "FavoritesFreelancer",
                newName: "Favorites");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesFreelancer_ClientId",
                table: "Favorites",
                newName: "IX_Favorites_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_ClientId",
                table: "Favorites",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
