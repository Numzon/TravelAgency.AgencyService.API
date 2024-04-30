using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgencyService.Adapter.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class agency_service_domain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankAccountDataId",
                table: "TravelAgencyAccount",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyData_AgencyName",
                table: "TravelAgencyAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TravelAgencyAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BankAccountData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Swift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalData_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalData_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalData_Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelAgencyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manager_TravelAgencyAccount_TravelAgencyId",
                        column: x => x.TravelAgencyId,
                        principalTable: "TravelAgencyAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelAgencyId = table.Column<int>(type: "int", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_TravelAgencyAccount_TravelAgencyId",
                        column: x => x.TravelAgencyId,
                        principalTable: "TravelAgencyAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManagerReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    TravelAgencyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerReport_Manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagerReport_TravelAgencyAccount_TravelAgencyId",
                        column: x => x.TravelAgencyId,
                        principalTable: "TravelAgencyAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelAgencyAccount_BankAccountDataId",
                table: "TravelAgencyAccount",
                column: "BankAccountDataId",
                unique: true,
                filter: "[BankAccountDataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ManagerId",
                table: "Comment",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TravelAgencyId",
                table: "Comment",
                column: "TravelAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_TravelAgencyId",
                table: "Manager",
                column: "TravelAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerReport_ManagerId",
                table: "ManagerReport",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerReport_TravelAgencyId",
                table: "ManagerReport",
                column: "TravelAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelAgencyAccount_BankAccountData_BankAccountDataId",
                table: "TravelAgencyAccount",
                column: "BankAccountDataId",
                principalTable: "BankAccountData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelAgencyAccount_BankAccountData_BankAccountDataId",
                table: "TravelAgencyAccount");

            migrationBuilder.DropTable(
                name: "BankAccountData");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ManagerReport");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropIndex(
                name: "IX_TravelAgencyAccount_BankAccountDataId",
                table: "TravelAgencyAccount");

            migrationBuilder.DropColumn(
                name: "BankAccountDataId",
                table: "TravelAgencyAccount");

            migrationBuilder.DropColumn(
                name: "CompanyData_AgencyName",
                table: "TravelAgencyAccount");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TravelAgencyAccount");
        }
    }
}
