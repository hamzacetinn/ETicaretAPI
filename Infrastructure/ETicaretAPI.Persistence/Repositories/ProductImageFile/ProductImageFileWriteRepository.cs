using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persisitence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
