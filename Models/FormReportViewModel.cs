using System;
using System.Collections.Generic;

namespace EFormBuilder.Models
{
    public class FormReportViewModel
    {
        public string FormId { get; set; }
        public string FormTitle { get; set; }
        public int TotalSubmissions { get; set; }
        public DateTime LastSubmissionDate { get; set; }
        public List<DailySubmissionData> DailySubmissions { get; set; }
        public List<FormResponseViewModel> FormResponses { get; set; }
    }

    public class DailySubmissionData
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class FormResponseViewModel
    {
        public string Id { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedDate { get; set; }
        public Dictionary<string, string> Answers { get; set; }
    }
}