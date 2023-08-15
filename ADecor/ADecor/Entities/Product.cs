using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ADecor.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }


        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        public double Price { get; set; }

        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Color { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }


        [StringLength(255), Column(TypeName = "varchar(255)")]
        public string Image { get; set; }


        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        [Required]
        public virtual Brand Brand { get; set; }

        
        public virtual ICollection<Category> Categories { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
       
    }
}
