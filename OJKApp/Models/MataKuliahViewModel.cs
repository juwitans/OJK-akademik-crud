using System.ComponentModel.DataAnnotations;

namespace OJKApp.Models
{
    public class MataKuliahViewModel
    {
        [Key]
        public int id {  get; set; }
        [Required]
        [RegularExpression("[A-Z]{2}-[0-9]{3}$",ErrorMessage = "Kode MK harus dalam format ABC-123")]
        [Display(Name = "Kode MK")]
        public string kodeMatkul { get; set; }
        [Required]
        [Display(Name = "Nama MK")]
        public string namaMatkul { get; set; }
        [Range(1, 10, ErrorMessage = "Besar SKS harus di antara 1-10")]
        [Display(Name = "SKS")]
        public int sks { get; set; }
}
}
