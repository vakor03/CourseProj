using System;
using System.Collections.Generic;
using System.Linq;

namespace FindPathProc.Lib
{
    public class Algos
    {
        private double[,] _smMatrix;
        public double[,] directDistance {private get; set; }
        int _vertsCount;

        public Algos(double[,] smMatrix)
        {
            _smMatrix = smMatrix;
            _vertsCount = _smMatrix.GetLength(0);
        }

        public double Deikstra(int startId, int finishId, ref List<int> path)
        {
            PriorityQueue<int> frontier = new PriorityQueue<int>();
            frontier.AddElement(startId, 0);
            int[] cameFrom = new int[_vertsCount];
            double[] costSoFar = new double[_vertsCount];
            cameFrom[startId] = -1;
            for (int i = 0; i < _vertsCount; i++)
            {
                if (i == startId)
                {
                    costSoFar[i] = 0;
                }
                else
                {
                    costSoFar[i] = double.MaxValue;
                }
            }

            while (!frontier.IsEmpty())
            {
                int currentId = frontier.Dequeue();

                if (currentId == finishId)
                {
                    path = new List<int>();
                    currentId = finishId;
                    while (currentId!=-1)
                    {
                        path.Add(currentId);
                        currentId = cameFrom[currentId];
                    }
                    path.Reverse();
                    return costSoFar[finishId];
                }


                for (int j = 0; j < _vertsCount; j++)
                {
                    if (_smMatrix[currentId, j] != 0)
                    {
                        double newCost = costSoFar[currentId] + _smMatrix[currentId, j];
                        if (newCost < costSoFar[j])
                        {
                            costSoFar[j] = newCost;
                            double priority = newCost;
                            frontier.AddElement(j, priority);
                            cameFrom[j] = currentId;
                        }
                    }
                }
            }

            return costSoFar[finishId];
        }


        public double BellmanFord(int startId, int finishId, ref List<int> path)
        {
            double[] distance = new double[_vertsCount];
            List<int>[] shortestPath = new List<int>[_vertsCount];

            for (int i = 0; i < _vertsCount; i++)
            {
                if (i == startId)
                {
                    distance[i] = 0;
                    shortestPath[i] = new List<int> {i};
                }
                else
                {
                    distance[i] = double.MaxValue;
                }
            }

            for (int i = 0; i < _vertsCount; i++)
            {
                for (int j = 0; j < _vertsCount; j++)
                {
                    for (int k = 0; k < _vertsCount; k++)
                    {
                        if (_smMatrix[j, k] != 0)
                        {
                            if (distance[j] != double.MaxValue && distance[k] > distance[j] + _smMatrix[j, k])
                            {
                                distance[k] = distance[j] + _smMatrix[j, k];
                                shortestPath[k] = new List<int>(shortestPath[j]) {k};
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < _vertsCount; i++)
            {
                for (int j = 0; j < _vertsCount; j++)
                {
                    if (_smMatrix[i, j] != 0)
                    {
                        if (distance[i] != double.MaxValue && distance[j] > distance[i] + _smMatrix[i, j])
                        {
                            Console.WriteLine("Граф містить цикли від'ємної ваги.");
                        }
                    }
                }
            }

            path = shortestPath[finishId];
            return distance[finishId];
        }

        public double AStar(int startId, int finishId, ref List<int> path)
        {
            PriorityQueue<int> frontier = new PriorityQueue<int>();
            frontier.AddElement(startId, 0);
            int[] cameFrom = new int[_vertsCount];
            double[] costSoFar = new double[_vertsCount];
            cameFrom[startId] = -1;
            for (int i = 0; i < _vertsCount; i++)
            {
                if (i == startId)
                {
                    costSoFar[i] = 0;
                }
                else
                {
                    costSoFar[i] = double.MaxValue;
                }
            }

            while (!frontier.IsEmpty())
            {
                int currentId = frontier.Dequeue();

                if (currentId == finishId)
                {
                    path = new List<int>();
                    currentId = finishId;
                    while (currentId!=-1)
                    {
                        path.Add(currentId);
                        currentId = cameFrom[currentId];
                    }
                    path.Reverse();
                    return costSoFar[finishId];
                }


                for (int j = 0; j < _vertsCount; j++)
                {
                    if (_smMatrix[currentId, j] != 0)
                    {
                        double newCost = costSoFar[currentId] + _smMatrix[currentId, j];
                        if (newCost < costSoFar[j])
                        {
                            costSoFar[j] = newCost;
                            double priority = newCost + directDistance[j, finishId];
                            frontier.AddElement(j, priority);
                            cameFrom[j] = currentId;
                        }
                    }
                }
            }

            return costSoFar[finishId];
        }
    }
}