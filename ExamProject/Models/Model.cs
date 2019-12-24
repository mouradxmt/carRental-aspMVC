namespace ExamProject.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string NomModel { get; set; }

        public Marque Marque { get; set; }
        public int MarqueID { get; set; }
    }
}