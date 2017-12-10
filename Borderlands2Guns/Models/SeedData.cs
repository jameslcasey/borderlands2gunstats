using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Borderlands2Guns.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Borderlands2GunsContext(
                serviceProvider.GetRequiredService<DbContextOptions<Borderlands2GunsContext>>()))
            {
                // Look for any movies.
                if (context.Guns.Any())
                {
                    return;   // DB has been seeded
                }

                context.Guns.AddRange(
                    
                     new Guns
                     {
                            Accuracy = 1,
                            Brand = Brand.Bandit,
                            Chance = 1,
                            Damage = 1,
                            DamageOnTarget = 1,
                            ElementalDamageOnTargetTimesDamagePerSecondTimesChance = 1,
                            ElementalDamagePerSecondTimesChance = 1,
                            DamageTimesFireRate = 1,
                            DamageTimesFireRateTimesMagazineSize = 1,
                            DamageTimesFireRateTimesMagazineSizePerReloadSpeed =1,
                            ElementalDamagePerSecond = 0,
                            FireRate = 1,
                            Level = 1,
                            MagazineSize = 1,
                            Name = @"seed",
                            ReloadSpeed = 1,
                            Type = GunType.AssaultRifle
                     }
                 
                );
                context.SaveChanges();
            }
        }




    }
}
