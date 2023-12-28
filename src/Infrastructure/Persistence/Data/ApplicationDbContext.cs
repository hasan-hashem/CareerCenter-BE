using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;
using System.Reflection;

namespace Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Auditing> Auditings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
      

    }
}
