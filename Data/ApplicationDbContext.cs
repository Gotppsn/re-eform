using Microsoft.EntityFrameworkCore;
using EFormBuilder.Models.Entities;

namespace EFormBuilder.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; }
        public DbSet<FormSubmission> FormSubmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Form entity
            modelBuilder.Entity<Form>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ElementsJson).IsRequired();
            });

            // Configure the FormSubmission entity
            modelBuilder.Entity<FormSubmission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FormId).IsRequired();
                entity.Property(e => e.SubmittedDate).IsRequired();
                entity.Property(e => e.SubmittedBy).HasMaxLength(100);
                entity.Property(e => e.AnswersJson).IsRequired();

                // Configure the relationship between Form and FormSubmission
                entity.HasOne(e => e.Form)
                    .WithMany(f => f.FormSubmissions) // Updated from Submissions to FormSubmissions
                    .HasForeignKey(e => e.FormId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}