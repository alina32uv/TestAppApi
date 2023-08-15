using ADecor.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADecor.Endpoints.WishLists
{
    public class WishModel
    {
        [Key]
        public short WishId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Popularity { get; set; }

        public int ProductId { get; set; }

    }
}
