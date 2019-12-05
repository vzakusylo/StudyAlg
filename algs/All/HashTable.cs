using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static HashTable.Run;

namespace HashTable
{
    [TestClass]
    public class Run
    {
        [TestMethod]
        public void Main()
        {
            //EnumerableTask();
            //TasksQuestion();
        }


        private static void EnumerableTask()
        {
            List<int> items = new List<int> { 1, 2 };
            IEnumerable<int> query = items.Where(i =>
            {
                Console.WriteLine(i + ".");
                return true;
            });

            foreach (var i in query)
            {
                Console.WriteLine(i + "-");
            }

            Console.Write(">");

            foreach (var i in query)
            {
                Console.WriteLine(i + "-");
            }
        }

        private static void TasksQuestion()
        {
            var tasks = new List<Action>();

            for (int i = 0; i < 2; i++)
            {
                var i1 = i;
                tasks.Add(() => { Console.Write(i1 + "."); });
            }

            foreach (var action in tasks)
            {
                action();
            }
        }


        public class HashTableNodePair<TKey, TValue>
        {
            public HashTableNodePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public TValue Value { get; set; }
            public TKey Key { get; set; }
        }
    }
}

    public class HashTable<TKey, TValue>
    {
        private const double _fillFactor = 0.75;
        private int _maxItemsAtCurrentSize;
        private int _count;

        private HashTableArray<TKey, TValue> _array;

        public HashTable() : this(1000) { }

        public HashTable(int initialCapacity)
        {
            if (initialCapacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            }

            _array = new HashTableArray<TKey, TValue>(initialCapacity);

            _maxItemsAtCurrentSize = (int)(initialCapacity * _fillFactor) + 1;
        }

        public void Add(TKey key, TValue value)
        {
            if (_count >= _maxItemsAtCurrentSize)
            {
                HashTableArray<TKey, TValue> largerArray = new HashTableArray<TKey, TValue>(_array.Capacity * 2);

                foreach (HashTableNodePair<TKey, TValue> node in _array.Items)
                {
                    largerArray.Add(node.Key, node.Value);
                }
            }
        }
    }

    public class HashTableArray<TKey, TValue>
    {
        private HashTableArrayNode<TKey, TValue>[] _array;

        public HashTableArray(int capacity)
        {
            _array = new HashTableArrayNode<TKey, TValue>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                _array[i] = new HashTableArrayNode<TKey, TValue>();
            }
        }

        public void Add(TKey key, TValue value)
        {
            _array[GetIndex(key)].Add(key, value);
        }

        public void Update(TKey key, TValue value)
        {
            _array[GetIndex(key)].Update(key, value);
        }

        public bool Remove(TKey key)
        {
            return _array[GetIndex(key)].Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _array[GetIndex(key)].TryGetValue(key, out value);
        }

        public int Capacity => _array.Length;

        public void Clear()
        {
            foreach (var hashTableArrayNode in _array)
            {
                hashTableArrayNode.Clear();
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var hashTableArrayNode in _array)
                {
                    foreach (TValue value in hashTableArrayNode.Values)
                    {
                        yield return value;
                    }
                }
            }
        }

        public IEnumerable<HashTableNodePair<TKey, TValue>> Items { get; set; }

        private int GetIndex(TKey key)
        {
            throw new NotImplementedException();
        }
    }

    public class HashTableArrayNode<TKey, TValue>
    {
        private LinkedList<HashTableNodePair<TKey, TValue>> _items;

        public void Update(TKey key, TValue value)
        {
            bool updated = false;
            if (_items != null)
            {
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        pair.Value = value;
                        updated = true;
                        break; ;
                    }
                }
            }

            if (!updated)
            {
                throw new ArgumentException("The collection does not contains the key");
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (_items == null)
            {
                _items = new LinkedList<HashTableNodePair<TKey, TValue>>();
            }
            else
            {
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        throw new ArgumentException("The collection already contains the key");
                    }
                }
            }

            _items.AddFirst(new HashTableNodePair<TKey, TValue>(key, value));
        }

        public bool Remove(TKey key)
        {
            bool removed = false;
            if (_items != null)
            {
                LinkedListNode<HashTableNodePair<TKey, TValue>> current = _items.First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        _items.Remove(current);
                        removed = true;
                        break;
                    }

                    current = current.Next;
                }
            }

            return removed;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            bool found = false;

            if (_items != null)
            {
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        value = pair.Value;
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        public void Clear()
        {
            if (_items != null)
            {
                _items.Clear();
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> nodePair in _items)
                    {
                        yield return nodePair.Key;
                    }
                }
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> nodePair in _items)
                    {
                        yield return nodePair.Value;
                    }
                }
            }
        }

        public IEnumerable<HashTableNodePair<TKey, TValue>> Items
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> nodePair in _items)
                    {
                        yield return nodePair;
                    }
                }
            }
        }
    }




