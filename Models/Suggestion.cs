namespace BioWiseV2.Models
{
    public class Suggestion
    {
        public int Id { get; set; }

        // one to many (Consumer)
        public int? ConsumerId { get; set; }
        public virtual ICollection<Consumer>? Consumers { get; set; }

        // one to many (Product)
        public int? ProductId { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
