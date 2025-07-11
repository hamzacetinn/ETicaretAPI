using ETicaretAPI.Persisitence.Contexts;
using ETicaretAPI.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Persistence
{
    public class IDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
    {
        public ETicaretAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ETicaretAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(configConnection.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
