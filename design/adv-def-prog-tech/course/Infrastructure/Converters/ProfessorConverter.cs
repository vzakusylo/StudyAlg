using Course.Domain.Models;

namespace Course.Infrastructure
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