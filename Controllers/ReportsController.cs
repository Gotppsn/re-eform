using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFormBuilder.Models;

namespace EFormBuilder.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form(string id)
        {
            // In a real application, this data would be fetched from a database
            var viewModel = new FormReportViewModel
            {
                FormId = id,
                FormTitle = id switch
                {
                    "1" => "Customer Feedback",
                    "2" => "IT Support Request",
                    "3" => "Employee Onboarding",
                    "4" => "Leave Request",
                    "5" => "Equipment Request",
                    "6" => "Travel Expense Claim",
                    _ => "Unknown Form"
                },
                TotalSubmissions = 124,
                LastSubmissionDate = DateTime.Now.AddDays(-1),
                DailySubmissions = new List<DailySubmissionData>
                {
                    new DailySubmissionData { Date = DateTime.Now.AddDays(-6), Count = 12 },
                    new DailySubmissionData { Date = DateTime.Now.AddDays(-5), Count = 18 },
                    new DailySubmissionData { Date = DateTime.Now.AddDays(-4), Count = 15 },
                    new DailySubmissionData { Date = DateTime.Now.AddDays(-3), Count = 22 },
                    new DailySubmissionData { Date = DateTime.Now.AddDays(-2), Count = 16 },
                    new DailySubmissionData { Date = DateTime.Now.AddDays(-1), Count = 19 },
                    new DailySubmissionData { Date = DateTime.Now, Count = 22 }
                },
                FormResponses = GenerateMockResponses(id)
            };

            return View(viewModel);
        }

        private List<FormResponseViewModel> GenerateMockResponses(string formId)
        {
            var responses = new List<FormResponseViewModel>();
            
            // Generate 10 mock responses
            for (int i = 1; i <= 10; i++)
            {
                responses.Add(new FormResponseViewModel
                {
                    Id = $"resp-{i}",
                    SubmittedBy = $"user{i}@example.com",
                    SubmittedDate = DateTime.Now.AddDays(-i).AddHours(-Random.Shared.Next(0, 24)),
                    Answers = formId switch
                    {
                        "1" => new Dictionary<string, string> // Customer Feedback
                        {
                            { "Overall Satisfaction", (Random.Shared.Next(1, 6)).ToString() },
                            { "Product Quality Rating", (Random.Shared.Next(1, 6)).ToString() },
                            { "Customer Service Rating", (Random.Shared.Next(1, 6)).ToString() },
                            { "Comments", $"This is a sample comment for response {i}." }
                        },
                        "2" => new Dictionary<string, string> // IT Support
                        {
                            { "Issue Type", Random.Shared.Next(3) switch { 0 => "Hardware", 1 => "Software", _ => "Network" } },
                            { "Priority", Random.Shared.Next(3) switch { 0 => "Low", 1 => "Medium", _ => "High" } },
                            { "Description", $"This is a sample IT issue description for response {i}." }
                        },
                        _ => new Dictionary<string, string>
                        {
                            { "Question 1", $"Answer 1 for response {i}" },
                            { "Question 2", $"Answer 2 for response {i}" },
                            { "Question 3", $"Answer 3 for response {i}" }
                        }
                    }
                });
            }
            
            return responses;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}