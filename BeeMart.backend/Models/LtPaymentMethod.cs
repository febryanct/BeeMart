using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeMart.backend.Models
{
    [Table("LtPaymentMethod")]
    public class LtPaymentMethod
    {
        [Key]
        public int IdPaymentMethod { get; set; }

        [Required]
        [StringLength(50)]
        public string MethodName { get; set; } = null!;

        public bool Stsrc { get; set; }
        public DateTime Datein { get; set; }
        public string Userin { get; set; } = null!;
        public DateTime? Dateup { get; set; }
        public string? Userup { get; set; }
    }
}