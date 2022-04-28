
using System.ComponentModel.DataAnnotations;

namespace AppModel.Models


{   //seller -- vendedor
    public class Seller
    {
        public int Id { get; set; }

        [Required ( ErrorMessage= "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "enter a valid email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage ="{0} must de from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }
        

        //associaçao entre as class saller com department
        //um seller tem um department
        //um pra um
        public Department department { get; set; }

        //associar ao banco de dado com a chave estrangeira para
        //nao fica null no banco 
        public int DepartmentId { get; set; }
      

        //associaçao entre a class saller com SalesRecord
        //um vededor tem varias vendas
        //um pra muito
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        //construtor
        //gera o contrutores mas sei as coleçao
        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            this.department = department;
        }

        //Metodos adicionar e remover
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            //flitra entre as data initial e final
            //atraves do linq == where
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}

