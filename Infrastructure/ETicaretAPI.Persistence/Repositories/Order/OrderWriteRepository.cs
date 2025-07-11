using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persisitence;
using ETicaretAPI.Persisitence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    internal class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
