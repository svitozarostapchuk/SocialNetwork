using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Friendship_entity_properties_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frienships_AspNetUsers_FriendId",
                table: "Frienships");

            migrationBuilder.DropForeignKey(
                name: "FK_Frienships_UserProfile_UserProfileId",
                table: "Frienships");

            migrationBuilder.DropIndex(
                name: "IX_Frienships_FriendId",
                table: "Frienships");

            migrationBuilder.DropColumn(
                name: "UserReceiver",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserSender",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Frienships");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Frienships");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "Frienships",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FriendProfileId",
                table: "Frienships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Frienships_UserProfile_UserProfileId",
                table: "Frienships",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frienships_UserProfile_UserProfileId",
                table: "Frienships");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FriendProfileId",
                table: "Frienships");

            migrationBuilder.AddColumn<string>(
                name: "UserReceiver",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSender",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "Frienships",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "Frienships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Frienships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Frienships_FriendId",
                table: "Frienships",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Frienships_AspNetUsers_FriendId",
                table: "Frienships",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Frienships_UserProfile_UserProfileId",
                table: "Frienships",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
