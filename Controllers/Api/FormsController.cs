using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EFormBuilder.Data;
using EFormBuilder.Models;
using EFormBuilder.Models.Entities;

namespace EFormBuilder.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FormsController> _logger;

        public FormsController(ApplicationDbContext context, ILogger<FormsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/forms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Form>>> GetForms()
        {
            return await _context.Forms.ToListAsync();
        }

        // GET: api/forms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Form>> GetForm(string id)
        {
            var form = await _context.Forms.FindAsync(id);

            if (form == null)
            {
                return NotFound();
            }

            // Increment view count
            form.Views++;
            await _context.SaveChangesAsync();

            return form;
        }

        // POST: api/forms
        [HttpPost]
        public async Task<ActionResult<Form>> CreateForm(Form form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            form.Id = Guid.NewGuid().ToString();
            form.CreatedDate = DateTime.UtcNow;
            form.UpdatedDate = DateTime.UtcNow;

            _context.Forms.Add(form);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetForm), new { id = form.Id }, form);
        }

        // PUT: api/forms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForm(string id, Form form)
        {
            if (id != form.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingForm = await _context.Forms.FindAsync(id);
                if (existingForm == null)
                {
                    return NotFound();
                }

                existingForm.Title = form.Title;
                existingForm.Description = form.Description;
                existingForm.Status = form.Status;
                existingForm.ElementsJson = form.ElementsJson;
                existingForm.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/forms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForm(string id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormExists(string id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }
    }
}