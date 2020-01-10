using System;
using System.Collections.Generic;
using System.Text;

namespace NaiveBayesClassifiyer
{
    class NaiveBayes
    {
        private int Size { get; set; }
        private int Atributes { get; set; }
        public double[][][] ProbabilityInClass { get; set; }

        public NaiveBayes(string[][] dataSet)
        {
            Size = dataSet.Length;
            Atributes = dataSet[0].Length - 1; //without the class column


            // The Learning Phase
            // 0. class counts & class probs -> how many democrats and how many republicans 
            int[] classCounts = new int[2];  // democrat, republican
            CountClassValues(dataSet, classCounts);
            double[] classProbs = new double[2];
            classProbs[0] = classCounts[0] / Size;
            classProbs[1] = classCounts[1] / Size;

            // 1. compute probs in class for every attributes 
            ProbabilityInClass = new double[2][][];  // [class][atribute][frequencies]
            ComputeProbabilities(dataSet, ProbabilityInClass, classCounts);
            DisplayProbabilities(ProbabilityInClass);
           
        }

        private void CountClassValues(string[][] dataSet, int[] classCounts)
        {
            for (int i = 0; i < Size; ++i)
            {
                int c = GetClass(dataSet[i][0]);
                if (c == -1) continue;

                ++classCounts[c];
            }
        }

        private void ComputeProbabilities(string[][] dataSet, double[][][] probabilityInClass, int[] classCounts)
        {
            probabilityInClass[0] = new double[Atributes][];
            probabilityInClass[1] = new double[Atributes][];

            for (int i = 0; i < Atributes; i++)
            {
                probabilityInClass[0][i] = new double[3];
                probabilityInClass[1][i] = new double[3];
            }

            CountValuesFroEachAtribute(dataSet, probabilityInClass);
            CalcPropability(probabilityInClass, classCounts);
        }

        private void CountValuesFroEachAtribute(string[][] dataSet, double[][][] probabilityInClass)
        {
            for (int i = 0; i < Size; ++i)
            {
                int c = GetClass(dataSet[i][0]);
                if (c == -1) continue;

                for (int j = 0; j < Atributes; ++j) // atrributes
                {
                    int a = GetAtributeValue(dataSet[i][j]);
                    ++probabilityInClass[c][j][a];
                }
            }
        }

        private void CalcPropability(double[][][] probabilityInClass, int[] classCounts)
        {

            //P(Atribute[i]|Class[j])
            for (int c = 0; c < 2; ++c)
            {
                for (int a = 0; a < Atributes; ++a)
                    for (int i = 0; i < probabilityInClass[0][0].Length; ++i)
                        probabilityInClass[c][a][i] /= classCounts[c]; //[class][atribute][probability]
            }
        }

        private void DisplayProbabilities(double[][][] probabilityInClass)
        {
            // display probabilities
            Console.WriteLine("\nProbabilities in class for every atribute:");
            for (int c = 0; c < 2; ++c)
            {
                Console.Write("class: " + c + " :\n");
                for (int a = 0; a < Atributes; ++a)
                {
                    for (int i = 0; i < probabilityInClass[0][0].Length; ++i)
                        Console.Write(probabilityInClass[c][a][i].ToString("F2") + "  ");

                    Console.Write('\t');
                }
                Console.WriteLine("");
            }

        }

        private static int GetClass(string dataClass)
        {
            if (dataClass == "democrat")
            {
                return 0;
            }
            else if (dataClass == "republican")
            {
                return 1;
            }

            return -1;
        }

        private static int GetAtributeValue(string dataAtribute)
        {
            if (dataAtribute == "y")
            {
                return 1; //yes
            }
            else if (dataAtribute == "n")
            {
                return 0; //no
            }

            return 2;
        }
    }
}
