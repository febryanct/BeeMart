using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeMart.backend.Models
{
    [Table("MsUser")]
    public class MsUser
    {
        [Key]
        [StringLength(5)]
        public string IdUser { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(300)]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = null!;

        public bool Stsrc { get; set; }
        public DateTime Datein { get; set; }
        public string Userin { get; set; } = null!;
        public DateTime? Dateup { get; set; }
        public string? Userup { get; set; }
    }
}