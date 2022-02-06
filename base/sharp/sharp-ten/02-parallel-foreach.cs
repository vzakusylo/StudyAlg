using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace parallel_foreach
{
    [TestClass]
    public class UnitTest1
    {
        // https://www.gregbair.dev/posts/parallel-foreachasync/
        [TestMethod]
        public async  Task TestMethod1()
        {
            var nums = Enumerable.Range(0, 100).ToArray();

            await Parallel.ForEachAsync(nums, async (i, token) =>
            {
                Console.WriteLine($"Starting iteration {i}");
                await Task.Delay(1000, token);
                Console.WriteLine($"Finishing iteration {i}");
            });


            //Starting iteration 2
            //Starting iteration 0
            //Starting iteration 6
            //Starting iteration 1
            //Starting iteration 3
            //Starting iteration 4
            //Starting iteration 5
            //Starting iteration 7
            //Starting iteration 8
            //Starting iteration 9
            //Starting iteration 10
            //Starting iteration 11
            //Finishing iteration 8
            //Finishing iteration 7
            //Finishing iteration 0
            //Finishing iteration 4
            //Finishing iteration 2
            //Finishing iteration 3
            //Finishing iteration 9
            //Finishing iteration 6
            //Finishing iteration 5
            //Finishing iteration 1
            //Finishing iteration 10
            //Finishing iteration 11
            //Starting iteration 12
            //Starting iteration 13
            //Starting iteration 14
            //Starting iteration 15
            //Starting iteration 16
            //Starting iteration 17
            //Starting iteration 18
            //Starting iteration 19
            //Starting iteration 20
            //Starting iteration 21
            //Starting iteration 22
            //Starting iteration 23
            //Finishing iteration 22
            //Finishing iteration 13
            //Finishing iteration 19
            //Finishing iteration 12
            //Finishing iteration 14
            //Starting iteration 24
            //Starting iteration 26
            //Finishing iteration 16
            //Starting iteration 27
            //Starting iteration 29
            //Finishing iteration 20
            //Starting iteration 30
            //Finishing iteration 21
            //Finishing iteration 15
            //Finishing iteration 17
            //Starting iteration 31
            //Finishing iteration 23
            //Starting iteration 32
            //Starting iteration 33
            //Starting iteration 34
            //Finishing iteration 18
            //Starting iteration 28
            //Starting iteration 35
            //Starting iteration 25
            //Finishing iteration 34
            //Finishing iteration 30
            //Finishing iteration 28
            //Finishing iteration 32
            //Finishing iteration 25
            //Finishing iteration 29
            //Finishing iteration 31
            //Starting iteration 38
            //Starting iteration 37
            //Starting iteration 39
            //Starting iteration 41
            //Starting iteration 36
            //Starting iteration 40
            //Finishing iteration 24
            //Starting iteration 42
            //Finishing iteration 26
            //Starting iteration 44
            //Starting iteration 43
            //Finishing iteration 35
            //Starting iteration 45
            //Finishing iteration 33
            //Starting iteration 46
            //Finishing iteration 27
            //Starting iteration 47
            //Finishing iteration 40
            //Finishing iteration 42
            //Finishing iteration 43
            //Finishing iteration 47
            //Finishing iteration 45
            //Finishing iteration 46
            //Starting iteration 49
            //Finishing iteration 38
            //Starting iteration 51
            //Finishing iteration 36
            //Finishing iteration 37
            //Starting iteration 53
            //Starting iteration 52
            //Starting iteration 54
            //Starting iteration 55
            //Starting iteration 56
            //Finishing iteration 39
            //Starting iteration 57
            //Finishing iteration 41
            //Starting iteration 58
            //Finishing iteration 44
            //Starting iteration 48
            //Starting iteration 59
            //Starting iteration 50
            //Finishing iteration 52
            //Finishing iteration 51
            //Finishing iteration 49
            //Finishing iteration 53
            //Finishing iteration 55
            //Finishing iteration 59
            //Starting iteration 61
            //Starting iteration 62
            //Starting iteration 63
            //Starting iteration 60
            //Starting iteration 65
            //Starting iteration 64
            //Finishing iteration 58
            //Finishing iteration 56
            //Finishing iteration 57
            //Starting iteration 66
            //Starting iteration 67
            //Starting iteration 68
            //Finishing iteration 50
            //Finishing iteration 48
            //Finishing iteration 54
            //Starting iteration 70
            //Starting iteration 69
            //Starting iteration 71
            //Finishing iteration 70
            //Finishing iteration 64
            //Finishing iteration 66
            //Finishing iteration 61
            //Starting iteration 75
            //Starting iteration 72
            //Starting iteration 73
            //Starting iteration 74
            //Finishing iteration 69
            //Finishing iteration 68
            //Finishing iteration 65
            //Starting iteration 77
            //Starting iteration 76
            //Finishing iteration 71
            //Starting iteration 78
            //Starting iteration 79
            //Finishing iteration 63
            //Starting iteration 80
            //Finishing iteration 67
            //Starting iteration 81
            //Finishing iteration 60
            //Starting iteration 82
            //Finishing iteration 62
            //Starting iteration 83
            //Finishing iteration 81
            //Starting iteration 84
            //Finishing iteration 72
            //Starting iteration 85
            //Finishing iteration 79
            //Finishing iteration 76
            //Finishing iteration 82
            //Starting iteration 86
            //Starting iteration 87
            //Starting iteration 88
            //Finishing iteration 74
            //Finishing iteration 80
            //Starting iteration 89
            //Finishing iteration 73
            //Finishing iteration 77
            //Finishing iteration 83
            //Starting iteration 90
            //Finishing iteration 75
            //Starting iteration 91
            //Starting iteration 92
            //Finishing iteration 78
            //Starting iteration 95
            //Starting iteration 93
            //Starting iteration 94
            //Finishing iteration 87
            //Finishing iteration 86
            //Finishing iteration 85
            //Finishing iteration 92
            //Finishing iteration 93
            //Starting iteration 98
            //Starting iteration 96
            //Starting iteration 99
            //Finishing iteration 95
            //Finishing iteration 84
            //Finishing iteration 94
            //Finishing iteration 89
            //Finishing iteration 88
            //Finishing iteration 90
            //Starting iteration 97
            //Finishing iteration 91
            //Finishing iteration 97
            //Finishing iteration 99
            //Finishing iteration 98
            //Finishing iteration 96
        }
    }

}
