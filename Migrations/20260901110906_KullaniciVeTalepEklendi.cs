using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KasaAPI.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciVeTalepEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SifreHash",
                table: "Kullanicilar",
                newName: "Sifre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sifre",
                table: "Kullanicilar",
                newName: "SifreHash");
        }
    }
}
