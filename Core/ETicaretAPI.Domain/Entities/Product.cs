﻿using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persisitence;

namespace ETicaretAPI.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductImageFile> ProductImageFiles { get; set; }
    }
}
