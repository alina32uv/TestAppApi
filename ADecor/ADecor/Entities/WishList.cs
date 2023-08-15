using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADecor.Entities
{
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short WishId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public int Popularity { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
