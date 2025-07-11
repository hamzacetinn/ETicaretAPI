using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Persisitence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
