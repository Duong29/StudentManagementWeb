using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementWeb.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Tuổi không được để trống!")]
        [Range(15,100, ErrorMessage = "Tuổi phải nằm trong khoảng từ 18 đến 100")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Tên lớp không được để trống!")]
        [StringLength(50)]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Gpa không được để trống!")]
        [Range(0,4, ErrorMessage = "Gpa trong khoảng từ 0 đến 4!")]
        [Column(TypeName = "decimal(3,2)")]
        public decimal? Gpa { get; set; }
    }
}
