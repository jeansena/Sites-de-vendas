namespace AppModel.Models
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> departments { get; set; }
    }
}
