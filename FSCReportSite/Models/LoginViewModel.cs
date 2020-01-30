using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FSCReportSite.Models
{
    public class LoginViewModel
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownika jest wymagane")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Compare("Password", ErrorMessage = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
