using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty ;

        [Column(TypeName = "decimal(18,2)")] //type decimal with upto 18 characters and only 2 decimals. 
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")] 
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment> ();
    }
}
