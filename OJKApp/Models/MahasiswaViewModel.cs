using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace OJKApp.Models
{
    public class MahasiswaViewModel
    {
        [Key]
        public int id { get; set; }
        [RegularExpression("[0-9]{9}$", ErrorMessage = "NIM harus berupa 9 digit angka")]
        public string nim {  get; set; }
        [Required]
        public string namaMhs {  get; set; }
        [Required]
        public DateOnly tglLahir {  get; set; }
        [Required]
        public string alamat {  get; set; }
        [Required]
        public JenisKelaminEnum jenisKelamin {  get; set; }

        public enum JenisKelaminEnum
        {
            [EnumMember(Value = "Perempuan")]
            Perempuan,
            [EnumMember(Value = "Laki-Laki")]
            LakiLaki
        }
    }
}
