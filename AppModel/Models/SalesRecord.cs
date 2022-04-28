using AppModel.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppModel.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Amount { get; set; }

        //associaçao com a class enums..
        public SaleStatus Status { get; set; }

        //associaçao da class seller com a class salesrecord
        //uma venda pode tem um vendedor
        // um pra um
        public Seller Seller { get; set; }

        //construtor
        //gera os construtores mas sem as col
        public SalesRecord()
        {
        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }

    }  
}
