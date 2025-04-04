using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace EFormBuilder.Models.Entities
{
    public class FormSubmission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        [Required]
        public string FormId { get; set; }
        
        [Required]
        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;
        
        [StringLength(100)]
        public string SubmittedBy { get; set; }
        
        [Required]
        public string AnswersJson { get; set; }
        
        [NotMapped]
        public Dictionary<string, string> Answers
        {
            get => string.IsNullOrEmpty(AnswersJson) 
                ? new Dictionary<string, string>() 
                : JsonSerializer.Deserialize<Dictionary<string, string>>(AnswersJson);
            set => AnswersJson = JsonSerializer.Serialize(value);
        }
        
        // Navigation property
        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }
    }
}