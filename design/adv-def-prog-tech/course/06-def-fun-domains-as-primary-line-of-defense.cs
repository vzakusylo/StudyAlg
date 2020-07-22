using Course.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Course
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

    // Comparing guards
    //      Domain Rules                            Algebraic Rules
    //       - Student applying for an exam          - Depositing money
    //       - Enroll semester                       - No zero amount
    //       - Have a prior grade of subject         - No negative amount 
    //       - Administrator teaches the subject     - 
    //       - 

}
