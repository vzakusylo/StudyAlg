using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sharp_nine_init_only_setter
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
   

    //Init-only setter
    public class User
    {
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }

        public User(string first, string last) => (FirstName, LastName) = (first, last);

        public User() { }
        public User(string firstName)
        {
            this.FirstName = firstName;
        }

        public void ChangeName(string name)
        {
            // can only be assigned in an object initializer  
           // this.FirstName = FirstName;
        }

        public void Demo()
        {
            User user = new User() { FirstName = "Name" };
            //user.FirstName = "NewName";
        }

        // Deconstruct feature
        public void DeconstructFeature()
        {
            var user = new User() { FirstName = "Name", MiddleName = "MiddleName", LastName = "LastName" };
            //var (FirstName, lastName) = user;
            
            //public void Deconstruct(out string firstName, out string lastName)
            //{
            //    (firstName, lastName) = (FirstName, LastName);
            //}

            //user.FirstName = "NewName";
        }
    }
}
