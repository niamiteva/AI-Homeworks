using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KMeansClustering
{
    class DataSetManager
    {
        public static double[][] ParseDataToDouble(string fileName, int numberOfCols, string separator)
        {
            string[][] data = MatrixLoad(fileName, 2, separator);
            double[][] parsed = DMatrixCreate(CountFileLines(fileName), numberOfCols);
            for (int i = 0; i < data.Length; i++)
                for (int j = 0; j < 2; j++)
                    double.TryParse(data[i][j], out parsed[i][j]);

            return parsed;
        }

        public static string[][] MatrixLoad(string fileName, int numberOfCols, string separator)
        {
            //TODO: make it with unknown number of rows and cols in the file
            int numberOfRows = CountFileLines(fileName);
            string[][] result = MatrixCreate(numberOfRows, numberOfCols);
            string line = "";
            string[] tokens = null;

            using (FileStream ifs = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(ifs))
                {
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("//") == true)
                            continue;
                        tokens = line.Split(separator.ToCharArray());
                        for (int j = 0; j < numberOfCols; ++j)
                        {
                            int k = j;  // into tokens
                            result[i][j] = tokens[k];
                        }
                        ++i;
                    }
                    //sr.Close(); ifs.Close();
                }                   
            }

            return result;
        }

        public static int CountFileLines(string filePath)
        {
            int i = 0;
            using (FileStream ifs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader r = new StreamReader(ifs))
                {
                    while (r.ReadLine() != null) { i++; }
                    //r.Close(); ifs.Close();                  
                }
            }
            return i;
        }

        public static string[][] MatrixCreate(int rows, int cols)
        {
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new string[cols];
            return result;
        }
        public static double[][] DMatrixCreate(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        public static void MatrixShow(string[][] matrix, int dec, int wid)
        {
            //TODO: string builder

            int nRows = matrix.Length;
            int nCols = matrix[0].Length;
            for (int i = 0; i < nRows; ++i)
            {
                for (int j = 0; j < nCols; ++j)
                {
                    string x = matrix[i][j];
                    Console.Write(x.PadLeft(wid));
                }
                Console.WriteLine("");
                // Utils.VectorShow(mat[i], dec, wid);
            }
        }
    }
}
