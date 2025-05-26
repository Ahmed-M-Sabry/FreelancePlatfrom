using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancePlatfrom.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Review_Contract_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Contracts_ContractsId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ContractsId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ContractsId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ContractId",
                table: "Reviews",
                column: "ContractId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Contracts_ContractId",
                table: "Reviews",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Contracts_ContractId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ContractId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ContractsId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ContractsId",
                table: "Reviews",
                column: "ContractsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Contracts_ContractsId",
                table: "Reviews",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
