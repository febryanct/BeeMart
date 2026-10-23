using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeMart.backend.Models
{
    [Table("TrTransactionDetail")]
    public class TrTransactionDetail
    {
        [Key]
        [StringLength(36)]
        public string IdTransactionDetail { get; set; } = null!;

        [Required]
        [StringLength(36)]
        public string IdTransaction { get; set; } = null!;

        [Required]
        [StringLength(36)]
        public string IdProduct { get; set; } = null!;

        public int Qty { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubTotal { get; set; }

        public bool Stsrc { get; set; }
        public DateTime Datein { get; set; }
        public string Userin { get; set; } = null!;
        public DateTime? Dateup { get; set; }
        public string? Userup { get; set; }
    }
}