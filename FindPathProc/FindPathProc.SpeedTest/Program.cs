using System;
using FindPathProc.Lib;

namespace FindPathProc.SpeedTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 100;
            long iterations = 0;
            int[,] weightMatrix = GenerateWeightMatrix(n);
            int[,] distMatrix = GenerateDistanceMatrix(n);
            CustomGraph customGraph = new CustomGraph(weightMatrix, distMatrix);
            Algo algo = new Algo(customGraph, 0, n - 1);
            algo.Dijkstra(out _, out iterations);
            Console.WriteLine(iterations);
            bool _a = false;
            algo.BellmanFord(out _,ref _a, out iterations);
            Console.WriteLine(iterations);
            algo.AStar(out _, out iterations);
            Console.WriteLine(iterations);
            
        }

        private static int[,] GenerateWeightMatrix(int n)
        {
            int[,] result = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i==j)
                    {
                        result[i, j] = 0;
                    }else if(i+1==j)
                    {
                        result[i, j] = 2*n;
                    }
                    else
                    {
                        result[i, j] = 2*n+1;
                    }
                }
            }

            return result;
        }
        private static int[,] GenerateDistanceMatrix(int n)
        {
            int[,] result = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i==j)
                    {
                        result[i, j] = 0;
                    }else if(i+1==j)
                    {
                        result[i, j] = n+i;
                    }
                    else
                    {
                        result[i, j] = 2*n+i;
                    }
                }
            }

            return result;
        }
    }
}