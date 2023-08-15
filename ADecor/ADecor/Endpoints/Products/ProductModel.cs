using ADecor.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADecor.Endpoints.Products
{
    public class ProductModel
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public string Image { get; set; }
        public int BrandId { get; set; }
        public List<int> CategoryIds { get; set; }

    }
}
