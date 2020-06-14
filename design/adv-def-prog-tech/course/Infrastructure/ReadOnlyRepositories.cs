using AutoMapper;
using Course;
using Course.Models;

namespace adv_def_prog_tech._06_def_fun_domains_as_primary_line_of_defense.Infrastructure
{
    public static class ReadOnlyRepositories
    {
        static ReadOnlyRepositories()
        {
           // Mapper.Initialize(cfg => cfg.CreateMap<Professor, ProfessorViewModel>());
        }

        public static IReadOnlyRepository<ProfessorViewModel>
            CreateProfessorRepository() =>
                new ReadOnlyRepository<ProfessorViewModel, Models.Professor, CollegeModel>(() => new CollegeModel(), dbContext => dbContext.Professors);
    }
}
