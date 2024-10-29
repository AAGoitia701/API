using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        public string Industry { get; set; } = string.Empty;

        [Required]
        public long MarketCap { get; set; }
    }
}
