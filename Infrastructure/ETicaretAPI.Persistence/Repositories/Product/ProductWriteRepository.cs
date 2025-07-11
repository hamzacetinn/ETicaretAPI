using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persisitence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    internal class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
