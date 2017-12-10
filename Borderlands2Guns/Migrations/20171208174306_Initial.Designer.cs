﻿// <auto-generated />
using Borderlands2Guns.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Borderlands2Guns.Migrations
{
    [DbContext(typeof(Borderlands2GunsContext))]
    [Migration("20171208174306_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Borderlands2Guns.Models.Guns", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Accuracy");

                    b.Property<int>("Brand");

                    b.Property<decimal>("Chance");

                    b.Property<int>("Damage");

                    b.Property<decimal>("DamageOnTarget");

                    b.Property<decimal>("DamageOnTargetTimesDamagePerSecondTimesChance");

                    b.Property<decimal>("DamagePerSecondTimesChance");

                    b.Property<decimal>("DamageTimesFireRate");

                    b.Property<decimal>("DamageTimesFireRateTimesMagazineSize");

                    b.Property<decimal>("DamageTimesFireRateTimesMagazineSizePerReloadSpeed");

                    b.Property<int>("Element");

                    b.Property<decimal>("ElementalDamagePerSecond");

                    b.Property<decimal>("FireRate");

                    b.Property<int>("Level");

                    b.Property<int>("MagazineSize");

                    b.Property<string>("Name");

                    b.Property<decimal>("ReloadSpeed");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Guns");
                });
#pragma warning restore 612, 618
        }
    }
}
