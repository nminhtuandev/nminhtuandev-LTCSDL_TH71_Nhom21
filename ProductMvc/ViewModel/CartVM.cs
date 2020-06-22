using ProductMvc.Models;
namespace ProductMvc.ViewModel
{
    public class CartVM
    {
        public int ProductId;
        public string ProductName;
        public int Price;       
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}