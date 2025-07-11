using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Persisitence
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
