using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models.viewModel
{
    public class CarViewModel
    {
        public Voiture Voiture { get; set; }
        public IEnumerable<Marque> Marques { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public ApplicationUser User { get; set; }
    }
}