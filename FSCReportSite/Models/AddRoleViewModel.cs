﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FSCReportSite.Models
{
    public class AddRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}