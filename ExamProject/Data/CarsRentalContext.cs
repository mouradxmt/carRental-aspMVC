using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;

namespace ExamProject.Models
{
    public class CarsRentalContext : IdentityDbContext<ApplicationUser>
    {
        public CarsRentalContext (DbContextOptions<CarsRentalContext> options)
            : base(options)
        {
        }

        public DbSet<Voiture> Voiture { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}
