using System;
using System.Collections.Generic;
using System.IO;

namespace FindPathProc.Lib
{
    public class FileManager
    {
        private readonly CustomGraph _customGraph;
        private readonly int _startId;
        private readonly int _destId;
        private readonly (string name, string timeWorking, string distance, List<int> path)[] _results;
        private readonly String _pathToDir;

        public FileManager(CustomGraph customGraph, int startId, int destId,
            (string name, string timeWorking, string distance, List<int> path)[] results, string pathToDir)
        {
            _customGraph = customGraph;
            _startId = startId;
            _destId = destId;
            _results = results;
            _pathToDir = pathToDir;
        }

        public void Write()
        {
            String separator = ", ";
            DateTime dateTime = DateTime.Now;
            String fileName = $"{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}";
            using (StreamWriter streamWriter =
                new StreamWriter(new FileStream(_pathToDir + fileName + ".txt", FileMode.Create)))
            {
                streamWriter.WriteLine("Vertices count");
                streamWriter.WriteLine(_customGraph.VertCount);
                streamWriter.WriteLine();
                streamWriter.WriteLine("Weight matrix");
                PrintMatrix(streamWriter, _customGraph.MatrixWeight, separator);

                streamWriter.WriteLine();
                streamWriter.WriteLine("Distance matrix");
                PrintMatrix(streamWriter, _customGraph.MatrixDist, separator);

                streamWriter.WriteLine();
                streamWriter.WriteLine($"Start ID: {_startId}");
                streamWriter.WriteLine($"Finish ID: {_destId}");
                PrintResults(streamWriter, separator);
            }
        }

        private void PrintResults(StreamWriter streamWriter, String separator)
        {
            for (int i = 0; i < 3; i++)
            {
                streamWriter.WriteLine();
                streamWriter.WriteLine($"{_results[i].name}");
                streamWriter.WriteLine($"Time: {_results[i].timeWorking}, Distance: {_results[i].distance}");
                if (_results[i].path == null)
                {
                    _results[i].path = new List<int>();
                }

                streamWriter.WriteLine($"Path: {String.Join(separator, _results[i].path)}");
            }
        }

        private void PrintMatrix(StreamWriter streamWriter, int[,] matrix, String separator)
        {
            for (int i = 0; i < _customGraph.VertCount; i++)
            {
                for (int j = 0; j < _customGraph.VertCount; j++)
                {
                    streamWriter.Write(matrix[i, j]);
                    if (j != _customGraph.VertCount - 1)
                    {
                        streamWriter.Write(separator);
                    }
                }

                streamWriter.WriteLine();
            }
        }
    }
}