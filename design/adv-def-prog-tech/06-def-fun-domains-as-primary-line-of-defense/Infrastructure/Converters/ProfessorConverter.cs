using def_fun_domains_as_primary_line_of_defense.Models;

namespace adv_def_prog_tech._06_def_fun_domains_as_primary_line_of_defense.Infrastructure
{
    public class ProfessorConverter : IModelConverter<Professor, Models.Professor>
    {
        public void CopyChanges(Professor key, Models.Professor value)
        {
            throw new System.NotImplementedException();
        }

        public Professor ToModel(Models.Professor persised)
        {
            throw new System.NotImplementedException();
        }

        public Models.Professor ToPesisted(Professor obj)
        {
            throw new System.NotImplementedException();
        }
    }
}