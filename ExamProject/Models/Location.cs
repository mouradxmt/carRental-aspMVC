using System;

namespace ExamProject.Models
{
    public class Location
    {
        public int Id { get; set; }
        public Locataire Locataire { get; set; }
        public Voiture  Voiture{ get; set; }

        public DateTime DateDebut{ get; set; } 
        public DateTime DateFin{ get; set; }

        public Double PrixTotal { get; set; }

    }
}