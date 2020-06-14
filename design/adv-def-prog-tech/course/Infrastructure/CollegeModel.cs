using Microsoft.EntityFrameworkCore;
using Course.Infrastructure.Models;

namespace Course.Infrastructure
{
    public class CollegeModel : DbContext
    {
        public DbSet<Professor> Professors { get; internal set; }
    }
}