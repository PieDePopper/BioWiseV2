using BioWiseV2.Models;

namespace BioWiseV2.ViewModels
{
    public class WeeklyReportAndGoalViewModel
    {
        public List<Weekly_report> WeeklyReports { get; set; }
        public List<Goal> Goals { get; set; }
        public List<TransportUsage> TransportUsages { get; set; }
        public List<Consumer> Consumers { get; set; }
    }
}
