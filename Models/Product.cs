namespace BioWiseV2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public float CO2Emmision { get; set; }
        public float WaterFootprint { get; set; }
        public float EcologicalFootprint { get; set; }
        public string? Category { get; set; }
        public string? Image_link { get; set; }

        // one to many (Suggestion)
        public int? Suggestion_Id { get; set; }
        public virtual ICollection<Suggestion>? Suggestions { get; set; }

    }
}
