using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NaiveBayesClassifiyer
{
    class DataSetManager
    {
        public static string[][] MatrixLoad(string fileName, int numberOfRows, int numberOfCols, char separator)
        {
            //TODO: make it with unknown number of rows and cols in the file
            
            string[][] result = MatrixCreate(numberOfRows, numberOfCols);
            string line = "";
            string[] tokens = null;
            FileStream ifs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(ifs);

            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("//") == true)
                    continue;
                tokens = line.Split(separator);
                for (int j = 0; j < numberOfCols; ++j)
                {
                    int k = j;  // into tokens
                    result[i][j] = tokens[k];
                }
                ++i;
            }
            sr.Close(); ifs.Close();
            return result;
        }

        public static string[][] MatrixCreate(int rows, int cols)
        {
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new string[cols];
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
