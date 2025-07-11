using ETicaretAPI.Application.Repositories.InvoiceFile;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persisitence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    internal class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
