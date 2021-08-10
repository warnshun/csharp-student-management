using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    /// <summary>
    /// 班級枚舉
    /// </summary>
    public enum ClassNameEnum
    {
        [Display(Name = "未分配")]
        None,
        [Display(Name = "一年級")]
        FirstGrade,
        [Display(Name = "二年級")]
        SecondGrade,
        [Display(Name = "三年級")]
        GradeThree
    }
}
