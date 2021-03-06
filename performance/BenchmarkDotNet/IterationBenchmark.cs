
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IterationBenchmark
//{
//    // https://habr.com/ru/post/466931/

//    [TestClass]
//	public class UnitTest1
//	{
//		[TestMethod]

//		public void TestMethod1()
//		{
//			IterationBenchmark ib = new IterationBenchmark();
//			ib.Setup();
//			var summary = BenchmarkRunner.Run<SingleVsFirst>();
//		}
//	}

//	public class IterationBenchmark
//	{
//		private List<int> _list;
//		private int[] _array;

//		[Params(100000, 10000000)] public int N;

//		[GlobalSetup]
//		public void Setup()
//		{
//			const int MIN = 1;
//			const int MAX = 10;
//			Random rnd = new Random();
//			_list = Enumerable.Repeat(0, N).Select(i => rnd.Next(MIN, MAX)).ToList();
//			_array = _list.ToArray();
//		}

//		[Benchmark]
//		public int ForList()
//		{
//			int total = 0;
//			for (int i = 0; i < _list.Count; i++)
//			{
//				total += _list[i];
//			}

//			return total;
//		}

//		[Benchmark]
//		public int ForeachList()
//		{
//			int total = 0;
//			foreach (int i in _list)
//			{
//				total += i;
//			}

//			return total;
//		}

//		[Benchmark]
//		public int ForeachArray()
//		{
//			int total = 0;
//			foreach (int i in _array)
//			{
//				total += i;
//			}

//			return total;
//		}

//		[Benchmark]
//		public int ForArray()
//		{
//			int total = 0;
//			for (int i = 0; i < _array.Length; i++)
//			{
//				total += _array[i];
//			}

//			return total;
//		}
//	}
//}
