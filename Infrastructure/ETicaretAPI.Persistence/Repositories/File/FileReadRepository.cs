using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Persisitence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.File
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
