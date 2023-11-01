namespace BioWiseV2.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string? Personalgoal { get; set; }
        public bool Completed { get; set; }
        public int Impact { get; set; }

        // many to one (Consumer)
        public virtual Consumer? Consumer { get; set; }
        public int? ConsumerId { get; set; }

    }
}
