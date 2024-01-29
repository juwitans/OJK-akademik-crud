using System.ComponentModel.DataAnnotations;

namespace OJKApp.Models
{
    public class DosenViewModel
    {
        [Key]
        public int id { get; set; }
        [RegularExpression("[0-9]{12}$", ErrorMessage = "NIP harus berupa 12 digit angka")]
        [Display(Name = "NIP")]
        public string nip { get; set; }
        [Required]
        [Display(Name = "Nama Dosen")]
        public string namaDosen { get; set; }
    }
}
