using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sharp_ten_records_structs_type
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var person = new Person("Thomas", "Andersen");
            var personStruct = new PersonStruct("Thomas", "Andersen");

            var dog = new Pet("Cookie", 7) {Color = "Brown"};
        }
    }

    // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#record-structs
    // https://www.infoq.com/articles/records-c9-tugce-ozdeger/

    // Record types

    public record Pet(string Name, int Age)
    {
        public string Color { get; init; }
    }
    public record class Person 
    {
        public string LastName { get; set; }
        public string Firstname { get; set; }

        public Person(string First, string Last) => (Firstname, LastName) = (First, Last);
    }

    public record struct PersonStruct()
    {
        //public string LastName { get; init; } = null;
        //public string Firstname { get; init; } = null;

        public PersonStruct(string First, string Last) : this()
        {}
    }

   public record Teacher : Person
    {
        public string Subject { get; }

        public Teacher(string first, string last, string sub)
            : base(first, last) => Subject = sub;
    }
}
