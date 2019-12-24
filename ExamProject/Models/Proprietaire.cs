using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models
{
    public class Proprietaire 
    {

        public int Id { get; set; }

        

        public bool IsAgence { get; set; }
        
        public string NomAgence { get; set; }
        public virtual List<Voiture>  Voitures{ get; set; }





    }


   
}
