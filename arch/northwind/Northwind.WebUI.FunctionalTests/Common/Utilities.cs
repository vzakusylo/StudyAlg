using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Persistence;

namespace Northwind.WebUI.FunctionalTests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static void InitializeDbForTests(NorthwindDbContext context)
        {
            NorthwindInitializer.Initialize(context);
        }
    }
}
