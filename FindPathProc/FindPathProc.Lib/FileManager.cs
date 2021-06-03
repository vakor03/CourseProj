using System;
using System.Collections.Generic;
using System.IO;

namespace FindPathProc.Lib
{
    /// <summary>
    /// Contains methods to write CustomGraph and Algo results to file
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Initial graph
        /// </summary>
        private readonly CustomGraph _customGraph;

        /// <summary>
        /// Id of start vertex
        /// </summary>
        private readonly int _startId;

        /// <summary>
        /// Id of start vertex in path
        /// </summary>
        private readonly int _destId;

        /// <summary>
        /// Id of destination vertex in path
        /// </summary>
        private readonly (string name, string timeWorking, string distance, List<int> path)[] _results;

        /// <summary>
        /// Path to directory, where file will be added
        /// </summary>
        private String _pathToDir;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customGraph">initial graph</param>
        /// <param name="startId">Id of start vertex in path</param>
        /// <param name="destId">Id of destination vertex in path</param>
        /// <param name="results">results of path finding algorithms</param>
        /// <param name="pathToDir">path to directory</param>
        public FileManager(CustomGraph customGraph, int startId, int destId,
            (string name, string timeWorking, string distance, List<int> path)[] results, string pathToDir)
        {
            _customGraph = customGraph;
            _startId = startId;
            _destId = destId;
            _results = results;
            _pathToDir = pathToDir;
        }

        ///<summary>
        /// Prints graph and statistic of algorithms to file
        ///</summary>
        public void Write()
        {
            if (!Directory.Exists(_pathToDir))
            {
                if (!Directory.Exists("../Results"))
                {
                    Directory.CreateDirectory("../Results/");
                }

                _pathToDir = "../Results/";
            }

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

        /// <summary>
        /// Prints results of algorithms to file
        /// </summary>
        /// <param name="streamWriter">main StreamWriter</param>
        /// <param name="separator">symbols, which will separate parts of array</param>
        private void PrintResults(StreamWriter streamWriter, String separator)
        {
            for (int i = 0; i < 3; i++)
            {
                streamWriter.WriteLine();
                streamWriter.WriteLine($"{_results[i].name}");
                streamWriter.WriteLine($"Iterations: {_results[i].timeWorking}, Distance: {_results[i].distance}");
                if (_results[i].path == null)
                {
                    _results[i].path = new List<int>();
                }

                streamWriter.WriteLine($"Path: {String.Join(separator, _results[i].path)}");
            }
        }

        /// <summary>
        /// Prints matrix to file
        /// </summary>
        /// <param name="streamWriter">main StreamWriter</param>
        /// <param name="matrix">2-d array</param>
        /// <param name="separator">symbols, which will separate parts of array</param>
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