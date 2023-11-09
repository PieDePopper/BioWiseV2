    namespace BioWiseV2.Models
{
    public class Weekly_report
    {
        public int Id { get; set; }
        public int Weeknr { get; set; }
        public int? Transport_emission { get; set; }
        public int? Total { get; set; }
        public int? Difference { get; set; }

        //one to many (TransportUsage)
        public int? TransportusageId { get; set; }
        public virtual ICollection<TransportUsage>? TransportUsages { get; set; }
    }
}
