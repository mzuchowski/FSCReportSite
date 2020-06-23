using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FSCReportSite.Models
{
    public class EditRoleViewModel : Controller
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "Nazwa roli jest wymagana!")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}