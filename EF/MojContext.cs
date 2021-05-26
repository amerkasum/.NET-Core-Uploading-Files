using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vjezba14042021.Models;

namespace Vjezba14042021.EF
{
    public class MojContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=.;Database=Vjezba14042021;Trusted_Connection=true;MultipleActiveResultSets=true");
        }

        public DbSet<Korisnik> korisnici { get; set; }
    }
}
