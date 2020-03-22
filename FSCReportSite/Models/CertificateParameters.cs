using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSCReportSite.Models
{
    public partial class CertificateParameters
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa certyfikatu jest wymagana!")]
        [Display(Name = "Nazwa certyfikatu")]
        public string CertificateName { get; set; }

        [Required(ErrorMessage = "Wartość jest wymagana!")]
        [Display(Name = "Współczynnik FSC")]
        public float? ParameterFsc { get; set; }

        [Required(ErrorMessage = "Wartość jest wymagana!")]
        [Display(Name = "Współczynnik CW")]
        public float? ParameterCw { get; set; }
    }
}
