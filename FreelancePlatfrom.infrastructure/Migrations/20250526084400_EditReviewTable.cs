using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancePlatfrom.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ApplyTasks_ApplyTaskId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ApplyTaskId",
                table: "Reviews",
                newName: "ContractsId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ApplyTaskId",
                table: "Reviews",
                newName: "IX_Reviews_ContractsId");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Contracts_ContractsId",
                table: "Reviews",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Contracts_ContractsId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ContractsId",
                table: "Reviews",
                newName: "ApplyTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ContractsId",
                table: "Reviews",
                newName: "IX_Reviews_ApplyTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ApplyTasks_ApplyTaskId",
                table: "Reviews",
                column: "ApplyTaskId",
                principalTable: "ApplyTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
