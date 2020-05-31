using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace def_fun_domains_as_primary_line_of_defense
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            ExamApplicationBuilder builder = new ExamApplicationBuilder();
            var app = builder.Build();
        }
    }

}
