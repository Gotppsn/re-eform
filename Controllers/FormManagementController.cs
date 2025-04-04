using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFormBuilder.Models;

namespace EFormBuilder.Controllers
{
    public class FormManagementController : Controller
    {
        private readonly ILogger<FormManagementController> _logger;

        public FormManagementController(ILogger<FormManagementController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // In a real application, these would be fetched from a database
            var forms = new List<FormViewModel>
            {
                new FormViewModel
                {
                    Id = "1",
                    Title = "Customer Feedback",
                    Description = "Collect feedback from customers about our services",
                    Status = "Active",
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-2),
                    Views = 124,
                    Submissions = 56
                },
                new FormViewModel
                {
                    Id = "2",
                    Title = "IT Support Request",
                    Description = "Submit IT support and service requests",
                    Status = "Active",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    UpdatedDate = DateTime.Now.AddDays(-3),
                    Views = 215,
                    Submissions = 87
                },
                new FormViewModel
                {
                    Id = "3",
                    Title = "Employee Onboarding",
                    Description = "New employee information collection form",
                    Status = "Draft",
                    CreatedDate = DateTime.Now.AddDays(-20),
                    UpdatedDate = DateTime.Now.AddDays(-5),
                    Views = 0,
                    Submissions = 0
                },
                new FormViewModel
                {
                    Id = "4",
                    Title = "Leave Request",
                    Description = "Form for requesting time off and leave",
                    Status = "Active",
                    CreatedDate = DateTime.Now.AddDays(-25),
                    UpdatedDate = DateTime.Now.AddDays(-4),
                    Views = 312,
                    Submissions = 124
                },
                new FormViewModel
                {
                    Id = "5",
                    Title = "Equipment Request",
                    Description = "Request new equipment or replacements",
                    Status = "Active",
                    CreatedDate = DateTime.Now.AddDays(-30),
                    UpdatedDate = DateTime.Now.AddDays(-8),
                    Views = 98,
                    Submissions = 36
                },
                new FormViewModel
                {
                    Id = "6",
                    Title = "Travel Expense Claim",
                    Description = "Submit travel-related expense claims",
                    Status = "Archived",
                    CreatedDate = DateTime.Now.AddDays(-60),
                    UpdatedDate = DateTime.Now.AddDays(-45),
                    Views = 223,
                    Submissions = 84
                }
            };

            return View(forms);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}