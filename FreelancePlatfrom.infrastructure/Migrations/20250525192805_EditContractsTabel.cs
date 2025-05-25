using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancePlatfrom.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditContractsTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_JobPosts_JobPostId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "JobPostId",
                table: "Contracts",
                newName: "ApplyTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_JobPostId",
                table: "Contracts",
                newName: "IX_Contracts_ApplyTaskId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ApplyTasks_ApplyTaskId",
                table: "Contracts",
                column: "ApplyTaskId",
                principalTable: "ApplyTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ApplyTasks_ApplyTaskId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "ApplyTaskId",
                table: "Contracts",
                newName: "JobPostId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ApplyTaskId",
                table: "Contracts",
                newName: "IX_Contracts_JobPostId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_JobPosts_JobPostId",
                table: "Contracts",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
