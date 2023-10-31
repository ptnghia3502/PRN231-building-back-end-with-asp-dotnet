using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eBookStore.Repositories.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Role (Id, RoleName, IsDeleted, CreationDate) VALUES ('15a2f6c5-6f34-4d07-bce1-d9c67b9d418a', 'Admin', 0, '01-01-2023');");
            migrationBuilder.Sql("INSERT INTO Role (Id, RoleName, IsDeleted, CreationDate) VALUES ('f9136cd7-1ec7-4329-b8b6-79119ae9e0e3', 'Member', 0, '01-01-2023');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
