using System;
using System.Collections.Generic;
using System.Text;

namespace moq_collections
{
    class FinancialTarget
    {
        public void AddTargetPoints(MyArray toArray, int count)
        {
            for (int i = 0; i < count; i++)
            {
                toArray.Append(3 + 2 * i);
            }
        }

        public MyArray InitializePoints()
        {
            MyArray array = new MyArray();
            array.Append(3);
            return array;
        }
    }
}
