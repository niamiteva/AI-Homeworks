using System;
using System.Collections.Generic;
using System.Text;

namespace NaiveBayesClassifiyer
{
    class KFoldCrossValidator
    {
        static void RandomizeOrder(double[][] allData)
        {
            Random rnd = new Random(0);
            for (int i = 0; i < allData.Length; ++i)
            {
                int r = rnd.Next(i, allData.Length);
                double[] tmp = allData[r];
                allData[r] = allData[i];
                allData[i] = tmp;
            }
        }

        static double CrossValidate(int[] numNodes, double[][] allData, int numFolds, double learnRate, double momentum)
        {
            // return the mean classification error for a neural network on allData
            int[] cumWrongCorrect = new int[2]; // cumulative # wrong, # correct

            for (int k = 0; k < numFolds; ++k) // each fold
            {
                NeuralNetwork nn = new NeuralNetwork(numNodes[0], numNodes[1], numNodes[2]); // instantiate a NN
                nn.InitializeWeights(0); // don't forget this!
                double[][] trainData = GetTrainData(allData, numFolds, k); // get the training data for curr fold
                double[][] testData = GetTestData(allData, numFolds, k); // the test data for curr fold
                double[] bestWeights = nn.Train(trainData, 35, learnRate, momentum); // artificially small maxEpochs
                nn.SetWeights(bestWeights); // not  necessary with back-prop training
                int[] wrongCorrect = nn.WrongCorrect(testData); // get classification results
                double error = (wrongCorrect[0] * 1.0) / (wrongCorrect[0] + wrongCorrect[1]);
                cumWrongCorrect[0] += wrongCorrect[0]; // accumulate # wrong
                cumWrongCorrect[1] += wrongCorrect[1]; // accumulate # correct
                Console.Write("Fold = " + k + ": wrong = " + wrongCorrect[0] + " correct = " + wrongCorrect[1]);
                Console.WriteLine("    error = " + error.ToString("F4"));
            }

            return (cumWrongCorrect[0] * 1.0) / (cumWrongCorrect[0] + cumWrongCorrect[1]); // mean classification error
        }

        static double[][] GetTrainData(double[][] allData, int numFolds, int fold)
        {
            int[][] firstAndLastTest = GetFirstLastTest(allData.Length, numFolds); // first and last index of rows tagged as TEST data
            int numTrain = allData.Length - (firstAndLastTest[fold][1] - firstAndLastTest[fold][0] + 1); // tot num rows - num test rows
            double[][] result = new double[numTrain][];
            int i = 0; // index into result/test data
            int ia = 0; // index into all data
            while (i < result.Length)
            {
                if (ia < firstAndLastTest[fold][0] || ia > firstAndLastTest[fold][1]) // this is a TRAIN row
                {
                    result[i] = allData[ia];
                    ++i;
                }
                ++ia;
            }
            return result;
        }

        static double[][] GetTestData(double[][] allData, int numFolds, int fold)
        {
            // return a reference to TEST data
            int[][] firstAndLastTest = GetFirstLastTest(allData.Length, numFolds); // first and last index of rows tagged as TEST data
            int numTest = firstAndLastTest[fold][1] - firstAndLastTest[fold][0] + 1;
            double[][] result = new double[numTest][];
            int ia = firstAndLastTest[fold][0]; // index into all data
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = allData[ia]; // the test data indices are contiguous
                ++ia;
            }
            return result;
        }

        static int[][] GetFirstLastTest(int numDataItems, int numFolds)
        {
            // return[fold][firstIndex][lastIndex] for k-fold cross validation TEST data
            int interval = numDataItems / numFolds;  // if there are 32 data items and k = num folds = 3, then interval = 32/3 = 10
            int[][] result = new int[numFolds][]; // pair of indices for each fold
            for (int i = 0; i < result.Length; ++i)
                result[i] = new int[2];

            for (int k = 0; k < numFolds; ++k) // 0, 1, 2
            {
                int first = k * interval; // 0, 10, 20
                int last = (k + 1) * interval - 1; // 9, 19, 29 (should be 31)
                result[k][0] = first;
                result[k][1] = last;
            }

            result[numFolds - 1][1] = result[numFolds - 1][1] + numDataItems % numFolds; // 29->31
            return result;
        }
    }
}
