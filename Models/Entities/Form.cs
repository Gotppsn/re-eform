using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFormBuilder.Models.Entities
{
    public class Form
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [Required]
        public string Status { get; set; } = "Draft"; // Draft, Active, Archived
        
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        
        public int Views { get; set; } = 0;
        
        public int SubmissionCount { get; set; } = 0; // Renamed from Submissions
        public int Submissions { get; set; }
        
        // Store the form elements as JSON
        [Required]
        public string ElementsJson { get; set; }
        
        [NotMapped]
        public List<FormElement> Elements
        {
            get => string.IsNullOrEmpty(ElementsJson) 
                ? new List<FormElement>() 
                : JsonSerializer.Deserialize<List<FormElement>>(ElementsJson);
            set => ElementsJson = JsonSerializer.Serialize(value);
        }
        
        // Navigation property with a different name
        public virtual ICollection<FormSubmission> FormSubmissions { get; set; }
    }
    
    public class FormElement
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public string Placeholder { get; set; }
        public string HelpText { get; set; }
        public bool Required { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FormElementOption> Options { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AcceptedFiles { get; set; }
    }
    
    public class FormElementOption
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}