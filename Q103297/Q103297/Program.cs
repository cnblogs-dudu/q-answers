using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Q103297
{
    class Program
    {
        private static StreamWriter _writer = new StreamWriter(@"C:\temp\test.txt", true, Encoding.UTF8) { AutoFlush = true };

        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                await _writer.WriteLineAsync(i.ToString());
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                _writer.WriteLine(i);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
        }
    }
}
