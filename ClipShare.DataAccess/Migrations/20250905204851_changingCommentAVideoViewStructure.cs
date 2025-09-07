using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClipShare.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changingCommentAVideoViewStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_AppuserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Video_VideoID",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "Comment",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "AppuserId",
                table: "Comment",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "PosyedAt",
                table: "Comment",
                newName: "PostedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_VideoID",
                table: "Comment",
                newName: "IX_Comment_VideoId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Video_VideoId",
                table: "Comment",
                column: "VideoId",
                principalTable: "Video",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Video_VideoId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Comment",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Comment",
                newName: "AppuserId");

            migrationBuilder.RenameColumn(
                name: "PostedAt",
                table: "Comment",
                newName: "PosyedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_VideoId",
                table: "Comment",
                newName: "IX_Comment_VideoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                columns: new[] { "AppuserId", "VideoID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AppuserId",
                table: "Comment",
                column: "AppuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Video_VideoID",
                table: "Comment",
                column: "VideoID",
                principalTable: "Video",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
