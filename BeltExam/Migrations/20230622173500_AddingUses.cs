using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeltExam.Migrations
{
    public partial class AddingUses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Coupons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserCouponUses",
                columns: table => new
                {
                    UserCouponUsesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCouponUses", x => x.UserCouponUsesId);
                    table.ForeignKey(
                        name: "FK_UserCouponUses_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "CouponId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCouponUses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCouponUses_CouponId",
                table: "UserCouponUses",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCouponUses_UserId",
                table: "UserCouponUses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Users_UserId",
                table: "Coupons");

            migrationBuilder.DropTable(
                name: "UserCouponUses");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_UserId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coupons");
        }
    }
}
