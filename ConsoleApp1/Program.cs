using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution {



    // Complete the oddNumbers function below.
    static List<int> oddNumbers(int l, int r) {
        List<int> result = new List<int>();
        for (int i = l; i < r; i++)
        {
            if(i % 2 != 0)
            {
                result.Add(i);
            }
        }
        return result;

    }

    static void Main(string[] args) {
        

        int l = Convert.ToInt32(Console.ReadLine().Trim());

        int r = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> res = oddNumbers(l, r);
        
    }
}
