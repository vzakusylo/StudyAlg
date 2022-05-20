using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using shared;


namespace backspacestringcompare
{
    // https://leetcode.com/problems/backspace-string-compare/
    [TestClass]
    public class Solution
    {
       
        [TestMethod]
        public void Main()
        {
          
        }

        public bool BackspaceCompare(string s, string t)
        {
            int s_pointer = s.Count() - 1;
            int t_pointer = t.Count() - 1;
            int s_skip = 0;
            int t_skip = 0;

            while (s_pointer >= 0 || t_pointer >= 0)
            {

                while (s_pointer >= 0)
                {
                    if (s[s_pointer] == '#')
                    {
                        s_skip += 1;
                        s_pointer -= 1;

                    }
                    else if (s_skip > 0)
                    {
                        s_pointer -= 1;
                        s_skip -= 1;
                    }
                    else
                    {
                        break;
                    }
                }

                while (t_pointer >= 0)
                {
                    if (t[t_pointer] == '#')
                    {
                        t_skip += 1;
                        t_pointer -= 1;

                    }
                    else if (t_skip > 0)
                    {
                        t_pointer -= 1;
                        t_skip -= 1;
                    }
                    else
                    {
                        break;
                    }
                }

                if (s_skip >= 0 && t_skip >= 0 && s[s_pointer] != t[t_pointer])
                {
                    return false;
                }

                if (s_pointer >= 0 != t_pointer >= 0)
                {
                    return false;
                }

                s_pointer = -1;
                t_pointer = -1;


            }

            return true;


        }

    }
}
