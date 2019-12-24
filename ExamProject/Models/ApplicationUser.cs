using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models
{
    public class ApplicationUser :IdentityUser
    {
        public Proprietaire Proprietaire { get; set; }
        public Locataire Locataire { get; set; }
        public string Tele { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        [NotMapped]
        public bool IsAdmin { get; set; }


    }
}
