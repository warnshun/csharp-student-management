using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    /// <summary>
    /// 學生 Model
    /// </summary>
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "名字")]
        [Required(ErrorMessage = "尚未輸入名字"), MaxLength(50, ErrorMessage = "名字最大長度為 50")]
        public string Name { get; set; }

        [Display(Name = "班級")]
        [Required(ErrorMessage = "尚未選擇班級")]
        public ClassNameEnum? ClassName { get; set; }

        [Display(Name = "電子信箱")]
        [Required(ErrorMessage = "尚未輸入電子信箱")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "電子信箱格式不正確")]
        public string Email { get; set; }


        public string AvatarPath { get; set; }
    }
}
