using Course.Domain.ViewModel;

namespace Course.Infrastructure
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
