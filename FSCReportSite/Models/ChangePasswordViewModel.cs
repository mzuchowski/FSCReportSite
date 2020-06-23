using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FSCReportSite.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType((DataType.Password))]
        [Display(Name = "Aktualne hasło")]
        public string CurentPassword { get; set; }

        [Required]
        [DataType((DataType.Password))]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType((DataType.Password))]
        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword",ErrorMessage = "Hasła muszą być takie same!")]
        public string ConfirmPassword { get; set; }
    }
}
