namespace FindPathProc.Lib
{
    public class CustomGraph
    {
        public double[,] matrixSm;
        public double[,] directDist;
        public int vertsCount;

        public CustomGraph(double[,] matrixSm, double[,] directDist)
        {
            this.matrixSm = matrixSm;
            this.directDist = directDist;
            vertsCount = matrixSm.GetLength(0);
        }
    }
}