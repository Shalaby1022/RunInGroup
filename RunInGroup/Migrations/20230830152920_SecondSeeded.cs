using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunInGroup.Migrations
{
    /// <inheritdoc />
    public partial class SecondSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clubs_Addresss_AddressId",
                table: "clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_clubs_AppUser_AppUserId",
                table: "clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clubs",
                table: "clubs");

            migrationBuilder.RenameTable(
                name: "clubs",
                newName: "Clubs");

            migrationBuilder.RenameIndex(
                name: "IX_clubs_AppUserId",
                table: "Clubs",
                newName: "IX_Clubs_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_clubs_AddressId",
                table: "Clubs",
                newName: "IX_Clubs_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Addresss_AddressId",
                table: "Clubs",
                column: "AddressId",
                principalTable: "Addresss",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AppUser_AppUserId",
                table: "Clubs",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Addresss_AddressId",
                table: "Clubs");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AppUser_AppUserId",
                table: "Clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs");

            migrationBuilder.RenameTable(
                name: "Clubs",
                newName: "clubs");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_AppUserId",
                table: "clubs",
                newName: "IX_clubs_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_AddressId",
                table: "clubs",
                newName: "IX_clubs_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clubs",
                table: "clubs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_clubs_Addresss_AddressId",
                table: "clubs",
                column: "AddressId",
                principalTable: "Addresss",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_clubs_AppUser_AppUserId",
                table: "clubs",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }
    }
}
