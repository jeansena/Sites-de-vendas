namespace AppModel.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //associaçao da class depertment com a seller
        //um department tem varios vendedores 
        //um pra muitos
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        //construtor
        //gera os construtores mas sem as coleçao
        public Department()
        {

        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //metodos
        public void AddSeller(Seller selles)
        {
            Sellers.Add(selles); 
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(Seller => Seller.TotalSales(initial, final));
        }
    }
}
