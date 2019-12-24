using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models
{
    public class Voiture
    {
        public int Id { get; set; }


        public double PrixParJour { get; set; }

        public int Annee { get; set; }
       
        public int Kilometrage { get; set; }
        public Marque Marque { get; set; }
        public int MarqueID { get; set; }
        public Model Model { get; set; }
        public int ModelID { get; set; }
        public Proprietaire Proprietaire { get; set; }
        public string Couleur { get; set; }
        public string Immatriculation { get; set; }
        public string Description { get; set; }
        

        public string ImagePath { get; set; }
    }
}

