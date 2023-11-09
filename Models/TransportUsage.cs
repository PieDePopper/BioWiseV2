namespace BioWiseV2.Models
{
    public class TransportUsage
    {
        public int Id { get; set; }
        public string Transport_type { get; set; }
        public int Distance { get; set; }
        public int Emmission_KM { get; set; }


        // many to one (Consumer)
        public virtual Consumer? Consumer { get; set; }
        public int? ConsumerId { get; set; }

        //many to one (Weekly_report)
        public virtual Weekly_report? Weekly_report { get; set; }
        public int? Weekly_reportId { get; set; }


    }
}
