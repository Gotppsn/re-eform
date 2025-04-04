using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using EFormBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFormBuilder.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            
            // Look for any existing forms
            if (context.Forms.Any())
            {
                return; // DB has been seeded
            }

            // Create sample forms
            var forms = new Form[]
            {
                new Form
                {
                    Id = "1",
                    Title = "Customer Feedback",
                    Description = "Collect feedback from customers about our services",
                    Status = "Active",
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                    Views = 124,
                    Submissions = 56,
                    ElementsJson = JsonSerializer.Serialize(new List<FormElement>
                    {
                        new FormElement
                        {
                            Id = "name",
                            Type = "text",
                            Label = "Your Name",
                            Placeholder = "Enter your full name",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "email",
                            Type = "text",
                            Label = "Email Address",
                            Placeholder = "Enter your email address",
                            HelpText = "We will only use your email if you request a response.",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "satisfaction",
                            Type = "radio",
                            Label = "How satisfied are you with our service?",
                            HelpText = "",
                            Required = true,
                            Options = new List<FormElementOption>
                            {
                                new FormElementOption { Label = "Very Satisfied", Value = "5" },
                                new FormElementOption { Label = "Satisfied", Value = "4" },
                                new FormElementOption { Label = "Neutral", Value = "3" },
                                new FormElementOption { Label = "Dissatisfied", Value = "2" },
                                new FormElementOption { Label = "Very Dissatisfied", Value = "1" }
                            }
                        },
                        new FormElement
                        {
                            Id = "improvements",
                            Type = "textarea",
                            Label = "What can we do to improve our service?",
                            Placeholder = "Your suggestions for improvement",
                            HelpText = "",
                            Required = false
                        }
                    })
                },
                
                new Form
                {
                    Id = "2",
                    Title = "IT Support Request",
                    Description = "Submit IT support and service requests",
                    Status = "Active",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    UpdatedDate = DateTime.Now.AddDays(-3),
                    Views = 215,
                    Submissions = 87,
                    ElementsJson = JsonSerializer.Serialize(new List<FormElement>
                    {
                        new FormElement
                        {
                            Id = "requestorName",
                            Type = "text",
                            Label = "Your Name",
                            Placeholder = "Enter your full name",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "department",
                            Type = "text",
                            Label = "Department",
                            Placeholder = "Enter your department",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "issueType",
                            Type = "select",
                            Label = "Issue Type",
                            Placeholder = "Select issue type",
                            HelpText = "",
                            Required = true,
                            Options = new List<FormElementOption>
                            {
                                new FormElementOption { Label = "Hardware", Value = "hardware" },
                                new FormElementOption { Label = "Software", Value = "software" },
                                new FormElementOption { Label = "Network", Value = "network" },
                                new FormElementOption { Label = "Account/Access", Value = "access" },
                                new FormElementOption { Label = "Other", Value = "other" }
                            }
                        },
                        new FormElement
                        {
                            Id = "priority",
                            Type = "radio",
                            Label = "Priority Level",
                            HelpText = "",
                            Required = true,
                            Options = new List<FormElementOption>
                            {
                                new FormElementOption { Label = "Low", Value = "low" },
                                new FormElementOption { Label = "Medium", Value = "medium" },
                                new FormElementOption { Label = "High", Value = "high" },
                                new FormElementOption { Label = "Urgent", Value = "urgent" }
                            }
                        },
                        new FormElement
                        {
                            Id = "description",
                            Type = "textarea",
                            Label = "Issue Description",
                            Placeholder = "Please describe your issue in detail",
                            HelpText = "Be as specific as possible",
                            Required = true
                        }
                    })
                },
                
                new Form
                {
                    Id = "3",
                    Title = "Employee Onboarding",
                    Description = "New employee information collection form",
                    Status = "Draft",
                    CreatedDate = DateTime.Now.AddDays(-20),
                    UpdatedDate = DateTime.Now.AddDays(-5),
                    Views = 0,
                    Submissions = 0,
                    ElementsJson = JsonSerializer.Serialize(new List<FormElement>
                    {
                        new FormElement
                        {
                            Id = "firstName",
                            Type = "text",
                            Label = "First Name",
                            Placeholder = "Enter first name",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "lastName",
                            Type = "text",
                            Label = "Last Name",
                            Placeholder = "Enter last name",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "email",
                            Type = "text",
                            Label = "Email Address",
                            Placeholder = "Enter email address",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "startDate",
                            Type = "date",
                            Label = "Start Date",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "position",
                            Type = "text",
                            Label = "Position",
                            Placeholder = "Enter job position",
                            HelpText = "",
                            Required = true
                        },
                        new FormElement
                        {
                            Id = "department",
                            Type = "select",
                            Label = "Department",
                            Placeholder = "Select department",
                            HelpText = "",
                            Required = true,
                            Options = new List<FormElementOption>
                            {
                                new FormElementOption { Label = "IT", Value = "it" },
                                new FormElementOption { Label = "HR", Value = "hr" },
                                new FormElementOption { Label = "Finance", Value = "finance" },
                                new FormElementOption { Label = "Marketing", Value = "marketing" },
                                new FormElementOption { Label = "Operations", Value = "operations" }
                            }
                        }
                    })
                }
            };

            context.Forms.AddRange(forms);
            context.SaveChanges();

            // Add some sample submissions
            if (!context.FormSubmissions.Any())
            {
                var submissions = new FormSubmission[]
                {
                    new FormSubmission
                    {
                        Id = Guid.NewGuid().ToString(),
                        FormId = "1",
                        SubmittedBy = "john.doe@example.com",
                        SubmittedDate = DateTime.Now.AddDays(-1),
                        AnswersJson = JsonSerializer.Serialize(new Dictionary<string, string>
                        {
                            { "name", "John Doe" },
                            { "email", "john.doe@example.com" },
                            { "satisfaction", "4" },
                            { "improvements", "The service is great, but could be faster." }
                        })
                    },
                    new FormSubmission
                    {
                        Id = Guid.NewGuid().ToString(),
                        FormId = "1",
                        SubmittedBy = "jane.smith@example.com",
                        SubmittedDate = DateTime.Now.AddDays(-2),
                        AnswersJson = JsonSerializer.Serialize(new Dictionary<string, string>
                        {
                            { "name", "Jane Smith" },
                            { "email", "jane.smith@example.com" },
                            { "satisfaction", "5" },
                            { "improvements", "Everything is perfect!" }
                        })
                    },
                    new FormSubmission
                    {
                        Id = Guid.NewGuid().ToString(),
                        FormId = "2",
                        SubmittedBy = "bob.johnson@example.com",
                        SubmittedDate = DateTime.Now.AddDays(-3),
                        AnswersJson = JsonSerializer.Serialize(new Dictionary<string, string>
                        {
                            { "requestorName", "Bob Johnson" },
                            { "department", "Marketing" },
                            { "issueType", "software" },
                            { "priority", "medium" },
                            { "description", "Cannot access marketing database after recent update." }
                        })
                    }
                };

                context.FormSubmissions.AddRange(submissions);
                context.SaveChanges();
            }
        }
    }
}