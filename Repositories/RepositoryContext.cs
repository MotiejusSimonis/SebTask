using Microsoft.EntityFrameworkCore;
using SEBtask.Models.Entities;

namespace SEBtask.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
    }
}
