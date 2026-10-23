using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeMart.backend.Models
{
    [Table("TrTransaction")]
    public class TrTransaction
    {
        [Key]
        [StringLength(36)]
        public string IdTransaction { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string InvoiceNo { get; set; } = null!;

        public DateTime TransactionDate { get; set; }

        [Required]
        [StringLength(5)]
        public string IdUser { get; set; } = null!;

        public int IdPaymentMethod { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        public bool Stsrc { get; set; }
        public DateTime Datein { get; set; }
        public string Userin { get; set; } = null!;
        public DateTime? Dateup { get; set; }
        public string? Userup { get; set; }
    }
}