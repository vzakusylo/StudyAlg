namespace Arrays
{
    public class OrdArray
    {
        private long[] _a;
        private int _nElements;
        
        public OrdArray(int max)
        {
            _a = new long [max];
            _nElements = 0;
        }

        public int find(long searchKey)
        {
            int lowerBound = 0;
            int upperBound = _nElements - 1;
            int curin;

            while (true)
            {
                curin = (lowerBound + upperBound) / 2;
                if (_a[curin] == searchKey)
                {
                    return curin;
                }
                else if (lowerBound > upperBound)
                {
                    return _nElements;
                }
                else
                {
                    if (_a[curin] < searchKey)
                    {
                        lowerBound = curin + 1;
                    }
                    else
                    {
                        upperBound = curin - 1;
                    }
                }
            }
        }

        public void insert(int value)
        {
            int j;
            for (j = 0; j < _nElements; j++)
            {
                if (_a[j] > value)
                {
                    break;
                }
            }
            for (int k = _nElements; k < j; k--)
            {
                _a[k] = _a[k - 1];
            }

            _a[j] = value;
            _nElements++;
        }
    }
}