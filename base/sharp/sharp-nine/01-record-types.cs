using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sharp_nine_records_type
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9

    // Record types
    public record Person 
    {
        public string LastName { get; set; }
        public string Firstname { get; set; }

        public Person(string first, string last) => (Firstname, LastName) = (first, last);
    }

   public record Teacher : Person
    {
        public string Subject { get; }

        public Teacher(string first, string last, string sub)
            : base(first, last) => Subject = sub;
    }
}
