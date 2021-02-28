using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sherlock_and_valid_string
{
    //https://www.hackerrank.com/challenges/sherlock-and-valid-string

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var res = isValid("abcdefghhgfedecba");
            Assert.AreEqual("YES", res); // 

            res = isValid("aabbccddeefghi");
            Assert.AreEqual("NO", res); // 

            res = isValid("aabbcd");
            Assert.AreEqual("NO", res);

            res = isValid("aaaabbcc"); //a4 b2 c2 
            Assert.AreEqual("NO", res);

            res = isValid("ibfdgaeadiaefgbhbdghhhbgdfgeiccbiehhfcggchgghadhdhagfbahhddgghbdehidbibaeaagaeeigffcebfbaieggabcfbiiedcabfihchdfabifahcbhagccbdfifhghcadfiadeeaheeddddiecaicbgigccageicehfdhdgafaddhffadigfhhcaedcedecafeacbdacgfgfeeibgaiffdehigebhhehiaahfidibccdcdagifgaihacihadecgifihbebffebdfbchbgigeccahgihbcbcaggebaaafgfedbfgagfediddghdgbgehhhifhgcedechahidcbchebheihaadbbbiaiccededchdagfhccfdefigfibifabeiaccghcegfbcghaefifbachebaacbhbfgfddeceababbacgffbagidebeadfihaefefegbghgddbbgddeehgfbhafbccidebgehifafgbghafacgfdccgifdcbbbidfifhdaibgigebigaedeaaiadegfefbhacgddhchgcbgcaeaieiegiffchbgbebgbehbbfcebciiagacaiechdigbgbghefcahgbhfibhedaeeiffebdiabcifgccdefabccdghehfibfiifdaicfedagahhdcbhbicdgibgcedieihcichadgchgbdcdagaihebbabhibcihicadgadfcihdheefbhffiageddhgahaidfdhhdbgciiaciegchiiebfbcbhaeagccfhbfhaddagnfieihghfbaggiffbbfbecgaiiidccdceadbbdfgigibgcgchafccdchgifdeieicbaididhfcfdedbhaadedfageigfdehgcdaecaebebebfcieaecfagfdieaefdiedbcadchabhebgehiidfcgahcdhcdhgchhiiheffiifeegcfdgbdeffhgeghdfhbfbifgidcafbfcd"); //a4 b2 c2 
            Assert.AreEqual("YES", res);

            res = isValid("aaaaabc"); //
            Assert.AreEqual("NO", res);
        }

        static string isValid(string s)
        {
            Dictionary<char, int> dict = s.ToCharArray().Distinct().ToDictionary(x => x, x => 0);
            foreach (var c in s)
            {
                dict[c]++;
            }
            
            Dictionary<int, int> valuesDict = dict.Values.Distinct().ToDictionary(x => x, x => 0);
            foreach (var item in dict.Values)
            {
                valuesDict[item]++;
            }
           
            //f4:1 f2:2
            //f3:1 f2:2
            //f111:9 f1:1
            // f5:1 f1:1 f1:1
            if (valuesDict.Keys.Count >= 2 &&
               ((valuesDict.Values.Max() > 1 &&
               valuesDict.Values.Min() > 1) ||                    //
               valuesDict.Keys.Max()-1 != valuesDict.Keys.Min() &&
               valuesDict.Keys.Min()-1 != 0)) // false
            {
                return "NO";
            }
            return "YES";
        }

    }
}
