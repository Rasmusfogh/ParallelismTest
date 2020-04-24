using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelismTest
{
    class Program
    {
        public class MyItem
        {
            public MyItem(double a, double b, double c)
            {
                A = a;
                B = b;
                C = c;
            }
                
            public double varA { get => A; set{ A = value; } }
            public double varB { get => B; set { B = value; } }
            public double varC { get => C; set { C = value; } }

            private double A;
            private double B;
            private double C;
        };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch(); 
            const long N = 40000000;
            double[] A, B, C;
            A = new double[N];
            B = new double[N]; 
            C = new double[N];

            var MyCollection = new List<MyItem>();

            Random rand = new Random(); 
            
            for (int i = 0; i < N; i++)
            {
                
                A[i] = rand.Next();
                B[i] = rand.Next();
                C[i] = rand.Next();
                var newItem = new MyItem(A[i], B[i], C[i]);
                MyCollection.Add(newItem);
            }

            Console.WriteLine("Starts sequential for now."); stopwatch.Start();
            for (int i = 0; i < N; i++)
            {
                C[i] = A[i] * B[i];
            }
            stopwatch.Stop();

            Console.WriteLine("Sequential loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds); stopwatch.Reset();
            Console.WriteLine("Finished");

            Console.WriteLine("Starts Parallel sequential for now."); stopwatch.Start();
            Parallel.For(0, N, i =>
            {
                C[i] = A[i] * B[i];
            });
            stopwatch.Stop();

            Console.WriteLine("Parallel sequential loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds); stopwatch.Reset();
            Console.WriteLine("Finished");

            Console.WriteLine("Starts Parallel sequential for now."); stopwatch.Start();
            Parallel.ForEach(MyCollection, item =>
            {
                item.varC = item.varA * item.varB;
            });
            stopwatch.Stop();

            //Console.WriteLine($"Size of collection: {MyCollection.Count}");
            Console.WriteLine("Parallel foreach sequential loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds); stopwatch.Reset();
            Console.WriteLine("Finished");
        }
    }
}
