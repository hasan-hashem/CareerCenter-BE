using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }

        DbSet<Service> Services { get; }

        DbSet<Provider> Providers { get; }

        DbSet<Job> Jobs { get; }

        DbSet<Project> Projects { get; }

        DbSet<Offer> Offers { get; }

        DbSet<Comment> Comments { get; }

        DbSet<Auditing> Auditings { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
