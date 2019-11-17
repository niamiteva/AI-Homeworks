using System;

namespace TravelingSalesmanGA
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize population
            int numberOfCities = 500;
            int populationSize = numberOfCities*3; //number of individuals in the population
            Population curentPopulation = new Population(populationSize);
            curentPopulation.InitialiseFirstPopulation(numberOfCities);

            Console.WriteLine("Initial distance: " + curentPopulation.GetFittest().DistanceFitness);

            // Evolve population for 100 generations
            Random r = new Random();
            int secondPrint = r.Next(15, (numberOfCities * 5) / 3);
            int thirdPrint = r.Next((numberOfCities * 5) / 3, (numberOfCities * 5) / 2);
            int fourthPrint = r.Next((numberOfCities * 5) / 2, (numberOfCities * 5) - 15);

            curentPopulation = GeneticAlgorithm.EvolvePopulation(curentPopulation);
            for (int i = 0; i < numberOfCities*5; i++)
            {

                if (i == 10)
                {
                    Console.WriteLine("10th generation: " + curentPopulation.GetFittest().DistanceFitness);
                }

                if (i == secondPrint)
                {
                    Console.WriteLine($"Generation {i}: " + curentPopulation.GetFittest().DistanceFitness);
                }

                if (i == thirdPrint)
                {
                    Console.WriteLine($"Generation {i}: " + curentPopulation.GetFittest().DistanceFitness);
                }

                if (i == fourthPrint)
                {
                    Console.WriteLine($"Generation {i}: " + curentPopulation.GetFittest().DistanceFitness);
                }

                curentPopulation = GeneticAlgorithm.EvolvePopulation(curentPopulation);
            }

            // Print final results
            Console.WriteLine("Final distance: " + curentPopulation.GetFittest().DistanceFitness);
            Console.WriteLine("Solution:");
            Console.WriteLine(curentPopulation.GetFittest().ToString());

            Console.ReadKey();
        }
    }
}
