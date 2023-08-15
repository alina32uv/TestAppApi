﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ADecor.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
