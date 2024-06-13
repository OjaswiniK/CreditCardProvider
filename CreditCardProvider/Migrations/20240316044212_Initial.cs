using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCardProvider.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CCCustomers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTypeIdID = table.Column<int>(type: "int", nullable: false),
                    CardLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCCustomers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CCCustomers_CardTypes_CardTypeIdID",
                        column: x => x.CardTypeIdID,
                        principalTable: "CardTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CCTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CCAvailbleBalance = table.Column<int>(type: "int", nullable: false),
                    CCCustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CCTransactions_CCCustomers_CCCustomerId",
                        column: x => x.CCCustomerId,
                        principalTable: "CCCustomers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CCCustomers_CardTypeIdID",
                table: "CCCustomers",
                column: "CardTypeIdID");

            migrationBuilder.CreateIndex(
                name: "IX_CCTransactions_CCCustomerId",
                table: "CCTransactions",
                column: "CCCustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CCTransactions");

            migrationBuilder.DropTable(
                name: "CCCustomers");

            migrationBuilder.DropTable(
                name: "CardTypes");
        }
    }
}
