namespace BioWiseV2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Emmission { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }

        // one to many (Suggestion)
        public int? Suggestions_Id { get; set; }
        public virtual ICollection<Suggestion>? Suggestions { get; set; }

    }
}
