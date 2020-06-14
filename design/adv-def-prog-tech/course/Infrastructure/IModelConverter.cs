using Course.Models;

namespace adv_def_prog_tech._06_def_fun_domains_as_primary_line_of_defense.Infrastructure
{
    public interface IModelConverter<TModel, TPesistance> 
    {
        TPesistance ToPesisted(TModel obj);
        TModel ToModel(TPesistance persised);
        void CopyChanges(TModel key, TPesistance value);
    }
}