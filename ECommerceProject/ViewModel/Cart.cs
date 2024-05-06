namespace ECommerceProject.ViewModel
{
    public class Cart
    {
        public Cart()
        {
            Products = new List<ProductVM>();
        }
        public List<ProductVM> Products { get; set; }
    }
}
