using adv_def_prog_tech._06_def_fun_domains_as_primary_line_of_defense.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


namespace adv_def_prog_tech._06_def_fun_domains_as_primary_line_of_defense.Infrastructure
{
    public class CollegeModel : DbContext
    {
        public DbSet<Professor> Professors { get; internal set; }
    }
}