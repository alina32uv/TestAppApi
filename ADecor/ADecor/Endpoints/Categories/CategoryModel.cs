using ADecor.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADecor.Endpoints.Categories
{
    public class CategoryModel
    {
        [Key]
       
        public int CategoryId { get; set; }


        public string Name { get; set; }

       /* public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }*/
    }
}
