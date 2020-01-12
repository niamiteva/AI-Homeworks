using System;
using System.Collections.Generic;
using System.Text;

namespace NaiveBayesClassifiyer
{
    class NaiveBayes
    {
        private int Size { get; set; }
        private int Predictors { get; set; }
        public double[][][] ProbabilityInClass { get; set; }
        public double[] ProbabilitiesPerClass { get; set; }
        public double[] ClassProbabilities { get; set; }
        //public Dictionary<string, double[]> AttributesProbabilities { get; set; }
        //public Dictionary<string, Dictionary<string, double[]>> PredictorsProbabilities { get; set;} 

        public NaiveBayes(string[][] dataSet)
        {
            //Size = dataSet.Length;
            Predictors = dataSet[0].Length - 1; //without the class column

            ProbabilitiesPerClass = new double[2];
            ClassProbabilities = new double[2];
            ProbabilityInClass = new double[2][][];  // [class][atribute][frequencies]

            //AttributesProbabilities = new Dictionary<string, double[]>(); //attributeName, [] {play=yes, play=no}
            //PredictorsProbabilities = new Dictionary<string, Dictionary<string, double[]>>(); //predictorName, atribute probabilities
        }

        public void LearningPhase(string[][] dataSet)
        {
            Size = dataSet.Length;
            // The Learning Phase
            // class counts & class probs -> how many democrats and how many republicans 
            int[] classValuesCount = new int[2];  // democrat, republican
            CountClassValues(dataSet, classValuesCount);

            //P(ClassPlay=Yes) & P(ClassPlay=No).
            ClassProbabilities[0] = classValuesCount[0] / Size;
            ClassProbabilities[1] = classValuesCount[1] / Size;

            //compute probs in class for every attributes          
            ComputeProbabilities(dataSet, classValuesCount);
            //DisplayProbabilities(ProbabilityInClass);
        }

        public int[] ClassificationPhase(string[][] data)
        {
            Size = data.Length;
            //Console.WriteLine("Classification result for the test data:");

            int[] result = new int[2];

            for (int i = 0; i < data.Length; i++)
            {
                double[] nbProbability = new double[2];
                nbProbability[0] = 1;
                nbProbability[1] = 1;

                //P(x1, x2 …, xk | Classj) = P(x1 | Classj) × P(x2 | Classj) ×…× P(xk | Classj)
                int v = -1;
                for (int c = 0; c < 2; c++)
                {
                    for (int p = 1; p < Predictors; p++)
                    {
                        for (int a = 0; a < 3; a++)
                        {
                            v = GetAtributeValue(data[i][p]);
                            nbProbability[c] *= ProbabilityInClass[c][a][v];
                        }
                    }
                }

                string prediction = nbProbability[0] < nbProbability[1] ? "republican" : "democrat";
                //Console.WriteLine(prediction);

                if (prediction == data[i][0])
                    ++result[0]; //correct
                else
                    ++result[1]; //wrong
            }

            return result;
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

        private void ComputeProbabilities(string[][] dataSet, int[] classCounts)
        {
            ProbabilityInClass[0] = new double[Predictors][];
            ProbabilityInClass[1] = new double[Predictors][];

            for (int i = 0; i < Predictors; i++)
            {
                ProbabilityInClass[0][i] = new double[3];
                ProbabilityInClass[1][i] = new double[3];
            }

            CountValuesFroEachAtribute(dataSet);
            CalcPropability(classCounts);
        }

        private void CountValuesFroEachAtribute(string[][] dataSet)
        {
            for (int i = 0; i < Size; ++i)
            {
                int c = GetClass(dataSet[i][0]);
                if (c == -1) continue;

                for (int j = 0; j < Predictors; ++j) // atrributes
                {
                    int a = GetAtributeValue(dataSet[i][j]);
                    ++ProbabilityInClass[c][j][a];
                }
            }
        }

        private void CalcPropability(int[] classCounts)
        {
            //P(Atribute[i]|Class[j])
            for (int c = 0; c < 2; ++c)
            {
                for (int a = 0; a < Predictors; ++a)
                    for (int i = 0; i < ProbabilityInClass[0][0].Length; ++i)
                        ProbabilityInClass[c][a][i] /= classCounts[c]; //[class][atribute][probability]
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
        private void DisplayProbabilities(double[][][] probabilityInClass)
        {
            // display probabilities
            Console.WriteLine("\nProbabilities in class for every atribute:");
            for (int c = 0; c < 2; ++c)
            {
                Console.Write("class: " + c + " :\n");
                for (int a = 0; a < Predictors; ++a)
                {
                    for (int i = 0; i < probabilityInClass[0][0].Length; ++i)
                        Console.Write(probabilityInClass[c][a][i].ToString("F2") + "  ");

                    Console.Write('\t');
                }
                Console.WriteLine("");
            }

        }
    }
}
