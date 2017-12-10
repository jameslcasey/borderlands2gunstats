using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Borderlands2Guns.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DamagePerSecondTimesChance",
                table: "Guns",
                newName: "ElementalDamagePerSecondTimesChance");

            migrationBuilder.RenameColumn(
                name: "DamageOnTargetTimesDamagePerSecondTimesChance",
                table: "Guns",
                newName: "ElementalDamageOnTargetTimesDamagePerSecondTimesChance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ElementalDamagePerSecondTimesChance",
                table: "Guns",
                newName: "DamagePerSecondTimesChance");

            migrationBuilder.RenameColumn(
                name: "ElementalDamageOnTargetTimesDamagePerSecondTimesChance",
                table: "Guns",
                newName: "DamageOnTargetTimesDamagePerSecondTimesChance");
        }
    }
}
