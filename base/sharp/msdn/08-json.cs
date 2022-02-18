using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace msdn_07
{
    // https://blog.okyrylchuk.dev/system-text-json-features-in-the-dotnet-6

    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public async Task Main()
        {

            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            await using var output = Console.OpenStandardOutput();

            Product product = new() {Name = "car", Test = "nice"};
            await JsonSerializer.SerializeAsync<Product>(output, product, options);
        }

        public async Task Example1()
        {
            
        }

        public class  Product : IJsonOnDeserialized, IJsonOnSerializing
        {
            [JsonPropertyOrder(2)]
            public string Name { get; set; }
            [JsonPropertyOrder(1)]
            public string Test { get; set; }

            public void OnDeserialized() => Validate();
            public void OnSerializing() => Validate();

            private void Validate()
            {
                if (Name is null)
                    throw new InvalidOperationException("Name can't be null");
            }
        }

        public async Task Example2()
        {
           
        }

        public static async Task CallCodeThatUsesAwaitAsync()
        {
           
        }
    }
}
