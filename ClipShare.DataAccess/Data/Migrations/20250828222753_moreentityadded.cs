using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClipShare.DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class moreentityadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Category_CategoryID",
                table: "Video");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Channel_ChannelID",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "ChannelID",
                table: "Video",
                newName: "ChannelId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Video",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Video_ChannelID",
                table: "Video",
                newName: "IX_Video_ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_Video_CategoryID",
                table: "Video",
                newName: "IX_Video_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Video",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Video",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "VideoView",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfVisit = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Proxy = table.Column<bool>(type: "bit", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoView", x => new { x.AppUserId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_VideoView_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoView_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoView_VideoId",
                table: "VideoView",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Category_CategoryId",
                table: "Video",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Channel_ChannelId",
                table: "Video",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Category_CategoryId",
                table: "Video");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Channel_ChannelId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "VideoView");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "Video",
                newName: "ChannelID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Video",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Video_ChannelId",
                table: "Video",
                newName: "IX_Video_ChannelID");

            migrationBuilder.RenameIndex(
                name: "IX_Video_CategoryId",
                table: "Video",
                newName: "IX_Video_CategoryID");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "Video",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Category_CategoryID",
                table: "Video",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Channel_ChannelID",
                table: "Video",
                column: "ChannelID",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
