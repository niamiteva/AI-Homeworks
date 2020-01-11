using System;

namespace KMeansClustering
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = "..\\..\\..\\..\\Datasets\\normal\\normal.txt";
            double[][] normalData = DataSetManager.ParseDataToDouble(fileName, 2, ' ');

            Console.WriteLine("========= Data Clustering of file normal.txt");
            //KMeansAlgorithm.ShowData(normalData, 1, true, true);
            Console.WriteLine("Enter number of clusters: ");
            int numClusters = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nSetting numClusters to " + numClusters);

            int[] clustering = KMeansAlgorithm.Cluster(normalData, numClusters); // this is it

            Console.WriteLine("\nK-means clustering complete\n");

            Console.WriteLine("Raw data by cluster:\n");
            KMeansAlgorithm.ShowClustered(normalData, clustering, numClusters, 1);

            //==================== the second file ===============================

            fileName = "..\\..\\..\\..\\Datasets\\unbalance\\unbalance.txt";
            double[][] unbalanceData = DataSetManager.ParseDataToDouble(fileName, 2, ' ');

            Console.WriteLine("========= Data Clustering of file unbalance.txt");
            //KMeansAlgorithm.ShowData(normalData, 1, true, true);
            Console.WriteLine("Enter number of clusters: ");
            numClusters = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nSetting numClusters to " + numClusters);

            clustering = KMeansAlgorithm.Cluster(unbalanceData, numClusters); // this is it

            Console.WriteLine("\nK-means clustering complete\n");

            Console.WriteLine("Raw data by cluster:\n");
            KMeansAlgorithm.ShowClustered(unbalanceData, clustering, numClusters, 1);

            Console.ReadLine();

        }
    }
}
