using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace All
{
    //https://www.hackerrank.com/challenges/time-conversion/problem

    //Sample Input 0
    //07:05:45PM
    //Sample Output 0
    //19:05:45

    public static class TimeConverter
    {
        public static string Conversion(string s)
        {
            var timeParts = s.Split(':');
            var hourPart = int.Parse(timeParts[0]);
            var minutesPart = int.Parse(timeParts[1]);
            var secondPart = int.Parse(timeParts[2].Replace("AM", "").Replace("PM", ""));
            var minutesSecondsPart = $"{(minutesPart < 10 ? "0" + minutesPart : minutesPart.ToString()) }:{ (secondPart < 10 ? "0" + secondPart : secondPart.ToString())}";
            if (s.Contains("AM"))
            {
                return hourPart < 12 ? $"{(hourPart < 10 ? "0" + hourPart : hourPart.ToString())}:{minutesSecondsPart}" :
                    $"00:{minutesSecondsPart}";
            }
            else
            {
                return hourPart < 12
                    ? $"{hourPart + 12}:{minutesSecondsPart}"
                    : $"12:{minutesSecondsPart}";
            }
        }
    }

    [TestClass]
    public class FormatConverterTests
    {
        [TestMethod]
        public void BeforeMiddayTest()
        {
            var actual = TimeConverter.Conversion("07:05:45PM");
            Assert.AreEqual("19:05:45", actual);
        }

        [TestMethod]
        public void Test1()
        {
            var actual = TimeConverter.Conversion("12:40:22AM");
            Assert.AreEqual("00:40:22", actual);
        }

        [TestMethod]
        public void Test4()
        {
            var actual = TimeConverter.Conversion("12:45:54PM");
            Assert.AreEqual("12:45:54", actual);
        }
    }
}
