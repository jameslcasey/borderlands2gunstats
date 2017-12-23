using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Borderlands2Guns.Models
{
    public class Borderlands2GunsContext : DbContext
    {
        public Borderlands2GunsContext (DbContextOptions<Borderlands2GunsContext> options)
            : base(options)
        {
        }

        public DbSet<Borderlands2Guns.Models.Guns> Guns { get; set; }
        public DbSet<Borderlands2Guns.Models.MetricRanks> MetricRanks { get; set; }



    }
}
