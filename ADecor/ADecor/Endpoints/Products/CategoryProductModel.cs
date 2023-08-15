using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Products
{
    [Keyless]
    public class CategoryProductModel
    {
        public int Productid { get; set; }
        public int CategoryId { get; set; }
    }
}
