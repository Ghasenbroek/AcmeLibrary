using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeLibrary.Migrations
{
    public partial class setupv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookShelf",
                columns: table => new
                {
                    BookShelfId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNumber = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    ShelfNumber = table.Column<int>(type: "int", nullable: false),
                    SubSection = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShelf", x => x.BookShelfId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(200)", nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "varchar(200)", nullable: true),
                    Surname = table.Column<string>(type: "varchar(200)", nullable: true),
                    IdNumber = table.Column<string>(type: "varchar(200)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(13)", nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    OutstandingFineBalance = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Member_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Authors = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    BookLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "BookLocations",
                columns: table => new
                {
                    BookLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    BookShelfId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLocations", x => x.BookLocationId);
                    table.ForeignKey(
                        name: "FK_BookLocations_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLocations_BookShelf_BookShelfId",
                        column: x => x.BookShelfId,
                        principalTable: "BookShelf",
                        principalColumn: "BookShelfId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckedOutBook",
                columns: table => new
                {
                    CheckedOutBookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Returned = table.Column<bool>(type: "bit", nullable: true),
                    ExpectedReturnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quality = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckedOutBook", x => x.CheckedOutBookId);
                    table.ForeignKey(
                        name: "FK_CheckedOutBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckedOutBook_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLocations_BookId",
                table: "BookLocations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLocations_BookShelfId",
                table: "BookLocations",
                column: "BookShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookLocationId",
                table: "Books",
                column: "BookLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckedOutBook_BookId",
                table: "CheckedOutBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckedOutBook_UserID",
                table: "CheckedOutBook",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserID",
                table: "Employee",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserId",
                table: "Member",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookLocations_BookLocationId",
                table: "Books",
                column: "BookLocationId",
                principalTable: "BookLocations",
                principalColumn: "BookLocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLocations_Books_BookId",
                table: "BookLocations");

            migrationBuilder.DropTable(
                name: "CheckedOutBook");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookLocations");

            migrationBuilder.DropTable(
                name: "BookShelf");
        }
    }
}
