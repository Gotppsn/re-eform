using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFormBuilder.Models;

namespace EFormBuilder.Controllers
{
    public class FormBuilderController : Controller
    {
        private readonly ILogger<FormBuilderController> _logger;

        public FormBuilderController(ILogger<FormBuilderController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            // In a real application, you would fetch the form from a database
            // For now, we'll just return the Create view
            ViewData["FormId"] = id;
            return View("Create");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}