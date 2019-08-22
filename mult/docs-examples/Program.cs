using System;
using System.Threading;
using System.Threading.Tasks;

namespace docs_examples
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=netcore-2.2
    class Program
    {
        static void Main(string[] args)
        {
            Action<object> action = (object obj) =>
            {
                Console.WriteLine("Task={0}, obj={1}, Thread={2}", Task.CurrentId, obj, Thread.CurrentThread.ManagedThreadId);
            };

            // Create a task but do not start it
            Task t1 = new Task(action, "alpha");

            // Construct a started task 
            Task t2 = Task.Factory.StartNew(action, "beta");
            // Block the main thread to demonstrate that t2 is executing.
            t2.Wait();

            // Launch t1
            t1.Start();
            Console.WriteLine("t1 has been launched. (Main Thread={0})", Thread.CurrentThread.ManagedThreadId);
            // Wait to the task to finish
            t1.Wait();

            // Construct a started task using Task.Run.
            var taskData = "delta";
            Task t3 = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task {Task.CurrentId}, obj={taskData}, Thread={Thread.CurrentThread.ManagedThreadId}");
            });
            // Wait for the task to finish
            t3.Wait();

            //Construct an unstarted task
            Task t4 = new Task(action, "gamma");
            // Run ins synchronously 
            t4.RunSynchronously();
            // Although the task was run synchronously, it is a good practice 
            // to wait for it in the event exceptions were thrown by the task.
            t4.Wait();
        }
    }
}
