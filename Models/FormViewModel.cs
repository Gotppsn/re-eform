using System;

namespace EFormBuilder.Models
{
    public class FormViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Views { get; set; }
        public int Submissions { get; set; }
    }
}