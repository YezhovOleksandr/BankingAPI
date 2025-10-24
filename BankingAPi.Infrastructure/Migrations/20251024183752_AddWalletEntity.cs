using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingAPi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWalletEntity_IdentityUsers_UserId",
                table: "UserWalletEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWalletEntity",
                table: "UserWalletEntity");

            migrationBuilder.RenameTable(
                name: "UserWalletEntity",
                newName: "UserWallets");

            migrationBuilder.RenameIndex(
                name: "IX_UserWalletEntity_WalletNumber",
                table: "UserWallets",
                newName: "IX_UserWallets_WalletNumber");

            migrationBuilder.RenameIndex(
                name: "IX_UserWalletEntity_UserId",
                table: "UserWallets",
                newName: "IX_UserWallets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWallets",
                table: "UserWallets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWallets_IdentityUsers_UserId",
                table: "UserWallets",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWallets_IdentityUsers_UserId",
                table: "UserWallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWallets",
                table: "UserWallets");

            migrationBuilder.RenameTable(
                name: "UserWallets",
                newName: "UserWalletEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserWallets_WalletNumber",
                table: "UserWalletEntity",
                newName: "IX_UserWalletEntity_WalletNumber");

            migrationBuilder.RenameIndex(
                name: "IX_UserWallets_UserId",
                table: "UserWalletEntity",
                newName: "IX_UserWalletEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWalletEntity",
                table: "UserWalletEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWalletEntity_IdentityUsers_UserId",
                table: "UserWalletEntity",
                column: "UserId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
