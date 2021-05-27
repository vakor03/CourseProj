namespace FindPathProc.Lib
{
    public class CustomGraph
    {
        public readonly int[,] MatrixWeight;
        public readonly int[,] MatrixDist;
        public readonly int VertCount;

        public bool HasNegativeEdges
        {
            get
            {
                for (int i = 0; i < VertCount; i++)
                {
                    for (int j = i; j < VertCount; j++)
                    {
                        if (MatrixWeight[i, j] < 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public CustomGraph(int[,] matrixWeight, int[,] matrixDist)
        {
            MatrixWeight = matrixWeight;
            MatrixDist = matrixDist;
            VertCount = matrixWeight.GetLength(0);
        }

        public bool PathExist(int start, int finish)
        {
            bool[] visited = new bool[VertCount];
            return PathExist(start, finish, visited);
        }

        private bool PathExist(int start, int finish, bool[] visited)
        {
            if (start == finish)
            {
                return true;
            }
            visited[start] = true;
            for (int i = start; i < VertCount; i++)
            {
                for (int j = 0; j < VertCount; j++)
                {
                    if (MatrixWeight[i, j] != 0 && !visited[j])
                    {
                        if (PathExist(j, finish, visited))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}