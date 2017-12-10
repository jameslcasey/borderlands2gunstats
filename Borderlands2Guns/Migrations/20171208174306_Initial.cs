using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Borderlands2Guns.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Accuracy = table.Column<decimal>(nullable: false),
                    Brand = table.Column<int>(nullable: false),
                    Chance = table.Column<decimal>(nullable: false),
                    Damage = table.Column<int>(nullable: false),
                    DamageOnTarget = table.Column<decimal>(nullable: false),
                    DamageOnTargetTimesDamagePerSecondTimesChance = table.Column<decimal>(nullable: false),
                    DamagePerSecondTimesChance = table.Column<decimal>(nullable: false),
                    DamageTimesFireRate = table.Column<decimal>(nullable: false),
                    DamageTimesFireRateTimesMagazineSize = table.Column<decimal>(nullable: false),
                    DamageTimesFireRateTimesMagazineSizePerReloadSpeed = table.Column<decimal>(nullable: false),
                    Element = table.Column<int>(nullable: false),
                    ElementalDamagePerSecond = table.Column<decimal>(nullable: false),
                    FireRate = table.Column<decimal>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    MagazineSize = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ReloadSpeed = table.Column<decimal>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guns");
        }
    }
}
