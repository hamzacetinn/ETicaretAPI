﻿using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Persisitence
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }
    }
}
