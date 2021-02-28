//using BenchmarkDotNet.Attributes;
//using BenchmarkDotNet.Attributes.Exporters;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections;
//using System.Reflection;
//using System.Security.Cryptography;

//namespace BenchmarkDotNet
//{
//   // https://github.com/dotnet/BenchmarkDotNet

//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
       
//        public void TestMethod1()
//        {
//            Md5VsSha256 m = new Md5VsSha256();
//            m.Setup();
//            m.Sha256();
            
//        }
//    }

//    //[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
//    //[SimpleJob(RuntimeMoniker.NetCoreApp30)]
//    //[SimpleJob(RuntimeMoniker.CoreRt30)]
//    //[SimpleJob(RuntimeMoniker.Mono)]


//    public class Md5VsSha256
//    {
//        private SHA256 sha256 = SHA256.Create();
//        private MD5 md5 = MD5.Create();
//        private byte[] data;

//        [Params(1000, 10000)]
//        public int N;

//        [GlobalSetup]
//        public void Setup()
//        {
//            data = new byte[N];
//            new Random(42).NextBytes(data);
//        }
       
//        [Benchmark]
//        public byte[] Sha256() => sha256.ComputeHash(data);

//        [Benchmark]
//        public byte[] Md5() => md5.ComputeHash(data);
//    }



//[AttributeUsage(AttributeTargets.Method)]
//    public class BenchmarkAttribute : Attribute
//    {
//    }

//    /// <summary>
//    /// Very simple benchmarking framework. Looks for all types
//    /// in the current assembly which have public static parameterless
//    /// methods marked with the Benchmark attribute. In addition, if 
//    /// there are public static Init, Reset and Check methods with
//    /// appropriate parameters (a string array for Init, nothing for
//    /// Reset or Check) these are called at appropriate times.
//    /// </summary>
//    public class Benchmark
//    {
//        /// <summary>
//        /// Number of times to run the methods in each type.
//        /// </summary>
//        static int runIterations = 1;

//        public static void Main(string[] args)
//        {
//            args = ParseCommandLine(args);

//            // Save all the benchmark classes from doing a nullity test
//            if (args == null)
//            {
//                args = new string[0];
//            }

//            // We're only ever interested in public static methods. This variable
//            // just makes it easier to read the code...
//            BindingFlags publicStatic = BindingFlags.Public | BindingFlags.Static;

//            foreach (Type type in Assembly.GetCallingAssembly().GetTypes())
//            {
//                // Find an Init method taking string[], if any
//                MethodInfo initMethod = type.GetMethod("Init", publicStatic, null,
//                                                      new Type[] { typeof(string[]) },
//                                                      null);

//                // Find a parameterless Reset method, if any
//                MethodInfo resetMethod = type.GetMethod("Reset", publicStatic,
//                                                       null, new Type[0],
//                                                       null);

//                // Find a parameterless Check method, if any
//                MethodInfo checkMethod = type.GetMethod("Check", publicStatic,
//                                                      null, new Type[0],
//                                                      null);

//                // Find all parameterless methods with the [Benchmark] attribute
//                ArrayList benchmarkMethods = new ArrayList();
//                foreach (MethodInfo method in type.GetMethods(publicStatic))
//                {
//                    ParameterInfo[] parameters = method.GetParameters();
//                    if (parameters != null && parameters.Length != 0)
//                    {
//                        continue;
//                    }

//                    if (method.GetCustomAttributes
//                        (typeof(BenchmarkAttribute), false).Length != 0)
//                    {
//                        benchmarkMethods.Add(method);
//                    }
//                }

//                // Ignore types with no appropriate methods to benchmark
//                if (benchmarkMethods.Count == 0)
//                {
//                    continue;
//                }

//                Console.WriteLine("Benchmarking type {0}", type.Name);

//                // If we've got an Init method, call it once
//                try
//                {
//                    if (initMethod != null)
//                    {
//                        initMethod.Invoke(null, new object[] { args });
//                    }
//                }
//                catch (TargetInvocationException e)
//                {
//                    Exception inner = e.InnerException;
//                    string message = (inner == null ? null : inner.Message);
//                    if (message == null)
//                    {
//                        message = "(No message)";
//                    }
//                    Console.WriteLine("Init failed ({0})", message);
//                    continue; // Next type
//                }

//                for (int i = 0; i < runIterations; i++)
//                {
//                    if (runIterations != 1)
//                    {
//                        Console.WriteLine("Run #{0}", i + 1);
//                    }

//                    foreach (MethodInfo method in benchmarkMethods)
//                    {
//                        try
//                        {
//                            // Reset (if appropriate)
//                            if (resetMethod != null)
//                            {
//                                resetMethod.Invoke(null, null);
//                            }

//                            // Give the test as good a chance as possible
//                            // of avoiding garbage collection
//                            GC.Collect();
//                            GC.WaitForPendingFinalizers();
//                            GC.Collect();

//                            // Now run the test itself
//                            DateTime start = DateTime.Now;
//                            method.Invoke(null, null);
//                            DateTime end = DateTime.Now;

//                            // Check the results (if appropriate)
//                            // Note that this doesn't affect the timing
//                            if (checkMethod != null)
//                            {
//                                checkMethod.Invoke(null, null);
//                            }

//                            // If everything's worked, report the time taken, 
//                            // nicely lined up (assuming no very long method names!)
//                            Console.WriteLine("  {0,-20} {1}", method.Name, end - start);
//                        }
//                        catch (TargetInvocationException e)
//                        {
//                            Exception inner = e.InnerException;
//                            string message = (inner == null ? null : inner.Message);
//                            if (message == null)
//                            {
//                                message = "(No message)";
//                            }
//                            Console.WriteLine("  {0}: Failed ({1})", method.Name, message);
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Parses the command line, returning an array of strings
//        /// which are the arguments the tasks should receive. This
//        /// array will definitely be non-null, even if null is
//        /// passed in originally.
//        /// </summary>
//        static string[] ParseCommandLine(string[] args)
//        {
//            if (args == null)
//            {
//                return new string[0];
//            }

//            for (int i = 0; i < args.Length; i++)
//            {
//                switch (args[i])
//                {
//                    case "-runtwice":
//                        runIterations = 2;
//                        break;

//                    case "-version":
//                        PrintEnvironment();
//                        break;

//                    case "-endoptions":
//                        // All following options are for the benchmarked
//                        // types.
//                        {
//                            string[] ret = new string[args.Length - i - 1];
//                            Array.Copy(args, i + 1, ret, 0, ret.Length);
//                            return ret;
//                        }

//                    default:
//                        // Don't understand option; copy this and
//                        // all remaining options and return them.
//                        {
//                            string[] ret = new string[args.Length - i];
//                            Array.Copy(args, i, ret, 0, ret.Length);
//                            return ret;
//                        }
//                }
//            }
//            // Understood all arguments
//            return new string[0];
//        }

//        /// <summary>
//        /// Prints out information about the operating environment.
//        /// </summary>
//        static void PrintEnvironment()
//        {
//            Console.WriteLine("Operating System: {0}", Environment.OSVersion);
//            Console.WriteLine("Runtime version: {0}", Environment.Version);
//        }
//    }
//}
