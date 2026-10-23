using System.ComponentModel.DataAnnotations;

namespace BeeMart.backend.DTO
{
    public class SubmitTransactionDto
    {
        [Required]
        public string IdUser { get; set; } = null!;

        [Required]
        public int IdPaymentMethod { get; set; }

        [Required]
        public List<TransactionItemDto> Items { get; set; } = new();
    }

    public class TransactionItemDto
    {
        [Required]
        public string IdProduct { get; set; } = null!;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}