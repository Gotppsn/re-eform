using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EFormBuilder.Data;
using EFormBuilder.Models.Entities;

namespace EFormBuilder.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SubmissionsController> _logger;

        public SubmissionsController(ApplicationDbContext context, ILogger<SubmissionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/submissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormSubmission>>> GetSubmissions()
        {
            return await _context.FormSubmissions.ToListAsync();
        }

        // GET: api/submissions/form/5
        [HttpGet("form/{formId}")]
        public async Task<ActionResult<IEnumerable<FormSubmission>>> GetFormSubmissions(string formId)
        {
            return await _context.FormSubmissions
                .Where(s => s.FormId == formId)
                .OrderByDescending(s => s.SubmittedDate)
                .ToListAsync();
        }

        // GET: api/submissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormSubmission>> GetSubmission(string id)
        {
            var submission = await _context.FormSubmissions.FindAsync(id);

            if (submission == null)
            {
                return NotFound();
            }

            return submission;
        }

        // POST: api/submissions
        [HttpPost]
        public async Task<ActionResult<FormSubmission>> CreateSubmission(FormSubmission submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate that the form exists
            var form = await _context.Forms.FindAsync(submission.FormId);
            if (form == null)
            {
                return BadRequest("Form does not exist");
            }

            submission.Id = Guid.NewGuid().ToString();
            submission.SubmittedDate = DateTime.UtcNow;

            _context.FormSubmissions.Add(submission);

            // Increment the form's submission count
            form.Submissions++;
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubmission), new { id = submission.Id }, submission);
        }

        // DELETE: api/submissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmission(string id)
        {
            var submission = await _context.FormSubmissions.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }

            // Get the form to decrement submission count
            var form = await _context.Forms.FindAsync(submission.FormId);
            if (form != null)
            {
                form.Submissions = Math.Max(0, form.Submissions - 1);
            }

            _context.FormSubmissions.Remove(submission);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}