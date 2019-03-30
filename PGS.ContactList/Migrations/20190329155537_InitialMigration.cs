using Microsoft.EntityFrameworkCore.Migrations;

namespace PGS.ContactList.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Surname = table.Column<string>(maxLength: 32, nullable: false),
                    Number = table.Column<string>(maxLength: 12, nullable: false),
                    Email = table.Column<string>(maxLength: 32, nullable: false),
                    PhotoString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
