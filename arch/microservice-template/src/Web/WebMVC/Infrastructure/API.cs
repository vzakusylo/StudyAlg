namespace WebMVC.Infrastructure
{
    public static class API
    {
        public static class Catalog
        {
            public static string GetAllBrands(string baseUri)
            {
                return $"{baseUri}reasoncodes";
            }
            
        }
    }
}