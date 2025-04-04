using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EFormBuilder.Data;
using EFormBuilder.Models.Entities;

namespace EFormBuilder.Services
{
    public interface IFormService
    {
        Task<IEnumerable<Form>> GetFormsAsync();
        Task<Form> GetFormByIdAsync(string id);
        Task<Form> CreateFormAsync(Form form);
        Task<Form> UpdateFormAsync(Form form);
        Task<bool> DeleteFormAsync(string id);
        Task<IEnumerable<FormSubmission>> GetFormSubmissionsAsync(string formId);
        Task<FormSubmission> SubmitFormAsync(FormSubmission submission);
    }

    public class FormService : IFormService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FormService> _logger;

        public FormService(ApplicationDbContext context, ILogger<FormService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Form>> GetFormsAsync()
        {
            return await _context.Forms
                .OrderByDescending(f => f.UpdatedDate)
                .ToListAsync();
        }

        public async Task<Form> GetFormByIdAsync(string id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form != null)
            {
                // Increment view count
                form.Views++;
                await _context.SaveChangesAsync();
            }
            return form;
        }

        public async Task<Form> CreateFormAsync(Form form)
        {
            form.Id = Guid.NewGuid().ToString();
            form.CreatedDate = DateTime.UtcNow;
            form.UpdatedDate = DateTime.UtcNow;

            _context.Forms.Add(form);
            await _context.SaveChangesAsync();

            return form;
        }

        public async Task<Form> UpdateFormAsync(Form form)
        {
            var existingForm = await _context.Forms.FindAsync(form.Id);
            if (existingForm == null)
            {
                return null;
            }

            existingForm.Title = form.Title;
            existingForm.Description = form.Description;
            existingForm.Status = form.Status;
            existingForm.ElementsJson = form.ElementsJson;
            existingForm.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingForm;
        }

        public async Task<bool> DeleteFormAsync(string id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form == null)
            {
                return false;
            }

            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FormSubmission>> GetFormSubmissionsAsync(string formId)
        {
            return await _context.FormSubmissions
                .Where(s => s.FormId == formId)
                .OrderByDescending(s => s.SubmittedDate)
                .ToListAsync();
        }

        public async Task<FormSubmission> SubmitFormAsync(FormSubmission submission)
        {
            // Validate that the form exists
            var form = await _context.Forms.FindAsync(submission.FormId);
            if (form == null)
            {
                return null;
            }

            submission.Id = Guid.NewGuid().ToString();
            submission.SubmittedDate = DateTime.UtcNow;

            _context.FormSubmissions.Add(submission);

            // Increment the form's submission count (renamed property)
            form.SubmissionCount++;
            
            await _context.SaveChangesAsync();

            return submission;
        }
    }
}