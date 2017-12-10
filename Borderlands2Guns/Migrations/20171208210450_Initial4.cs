using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Borderlands2Guns.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Element",
                table: "Guns");

            migrationBuilder.AddColumn<int>(
                name: "ElementalEffect",
                table: "Guns",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElementalEffect",
                table: "Guns");

            migrationBuilder.AddColumn<int>(
                name: "Element",
                table: "Guns",
                nullable: true);
        }
    }
}
