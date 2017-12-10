using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Borderlands2Guns.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllTypesDamageOnTargetRank",
                table: "Guns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllTypesElementalDamageOnTargetRank",
                table: "Guns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EachElementalTypeDamageOnTargetRank",
                table: "Guns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EachTypeDamageOnTargetRank",
                table: "Guns",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllTypesDamageOnTargetRank",
                table: "Guns");

            migrationBuilder.DropColumn(
                name: "AllTypesElementalDamageOnTargetRank",
                table: "Guns");

            migrationBuilder.DropColumn(
                name: "EachElementalTypeDamageOnTargetRank",
                table: "Guns");

            migrationBuilder.DropColumn(
                name: "EachTypeDamageOnTargetRank",
                table: "Guns");
        }
    }
}
