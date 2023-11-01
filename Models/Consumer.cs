namespace BioWiseV2.Models
{
    public class Consumer
    {
        public int Id { get; set; }

        // one to many (TransportUsage)
        public int? TransportUsageId { get; set; }
        public virtual ICollection<TransportUsage>? TransportUsages { get; set; }

        // one to many (Goal)
        public int? GoalId { get; set; }
        public virtual ICollection<Goal>? Goals { get; set; }

        // one to many (Suggestion)
        public int? SuggestionId { get; set; }
        public virtual ICollection<Suggestion>? Suggestions { get; set; }
    }
}
