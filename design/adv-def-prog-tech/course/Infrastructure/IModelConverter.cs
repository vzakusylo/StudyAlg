namespace Course.Infrastructure
{
    public interface IModelConverter<TModel, TPesistance> 
    {
        TPesistance ToPesisted(TModel obj);
        TModel ToModel(TPesistance persised);
        void CopyChanges(TModel key, TPesistance value);
    }
}