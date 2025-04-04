using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFormBuilder.Services;
using EFormBuilder.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EFormBuilder.Controllers
{
    public class FormViewController : Controller
    {
        private readonly IFormService _formService;
        private readonly ILogger<FormViewController> _logger;

        public FormViewController(IFormService formService, ILogger<FormViewController> logger)
        {
            _formService = formService;
            _logger = logger;
        }

        // GET: /FormView/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var form = await _formService.GetFormByIdAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            if (form.Status != "Active")
            {
                return RedirectToAction("FormNotAvailable");
            }

            return View(form);
        }

        // POST: /FormView/{id}/Submit
        [HttpPost("{id}/Submit")]
        public async Task<IActionResult> Submit(string id, [FromForm] Dictionary<string, string> formData)
        {
            var form = await _formService.GetFormByIdAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            if (form.Status != "Active")
            {
                return BadRequest("This form is not currently accepting submissions.");
            }

            // Remove the request verification token from form data
            formData.Remove("__RequestVerificationToken");

            // Create submission
            var submission = new FormSubmission
            {
                FormId = id,
                SubmittedBy = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous",
                Answers = formData
            };

            await _formService.SubmitFormAsync(submission);

            return RedirectToAction("ThankYou", new { id });
        }

        // GET: /FormView/{id}/ThankYou
        [HttpGet("{id}/ThankYou")]
        public async Task<IActionResult> ThankYou(string id)
        {
            var form = await _formService.GetFormByIdAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: /FormView/FormNotAvailable
        [HttpGet("FormNotAvailable")]
        public IActionResult FormNotAvailable()
        {
            return View();
        }
    }
}