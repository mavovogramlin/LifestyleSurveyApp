using LifestyleSurveyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifestyleSurveyApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; } = null!;
    }
}
