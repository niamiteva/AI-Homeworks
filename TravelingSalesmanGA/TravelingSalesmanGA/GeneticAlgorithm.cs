using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class GeneticAlgorithm
    {
        //public readonly double mutationPercent = 0.015;
        private static int tournamentSize = 5;
        //public Random randomIndex = new Random();

        public static Population EvolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.PopulationSize);

            // Crossover population
            // Loop over the new population's size and create individuals from
            // Current population
            for (int i = 0; i < newPopulation.PopulationSize; i++)
            {
                // Select parents
                Individual parent1 = TournamentSelection(pop);
                Individual parent2 = TournamentSelection(pop);
                // Crossover parents
                Individual child = Crossover(parent1, parent2);
                // Add child to new population
                newPopulation.SetIndividualAtIndex(i, child);
            }

            // Mutate the new population a bit to add some new genetic material
            //for (int i = elitismOffset; i < newPopulation.populationSize(); i++)
            //for (int i = 0; i < newPopulation.PopulationSize; i++)
            //{
            //    Mutate(newPopulation.GetInduvidualAtIndex(i));
            //}
            Mutate(newPopulation);

            return newPopulation;
        }

        private static Individual TournamentSelection(Population pop)
        {
            // Create a tournament population
            Population tournament = new Population(tournamentSize);
            Random r = new Random();
            // For each place in the tournament get a random candidate tour and
            // add it
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomId =r.Next(0, pop.PopulationSize);
                tournament.SetIndividualAtIndex(i, pop.GetInduvidualAtIndex(randomId));
            }
            // Get the fittest tour
            Individual fittest = tournament.GetFittest();

            return fittest;
        }

        public static Individual Crossover(Individual parent1, Individual parent2)
        {
            // Create new child tour
            Individual child = new Individual(parent1);
            Random r = new Random();

            // Get start and end sub tour positions for parent1's tour
            int startCrossPoint = r.Next(0, parent1.RouteSize);
            int endCrossPoint = r.Next(0, parent1.RouteSize);

            if(startCrossPoint < endCrossPoint)
            {
                for (int i = startCrossPoint; i < endCrossPoint; i++)
                {
                    child.SetCityAtIndex(i, parent1.GetCityAtIndex(i));
                }
            }
            else
            {
                for (int i = 0; i < endCrossPoint; i++)
                {
                    child.SetCityAtIndex(i, parent1.GetCityAtIndex(i));
                }

                for (int i = startCrossPoint; i < parent1.RouteSize; i++)
                {
                    child.SetCityAtIndex(i, parent1.GetCityAtIndex(i));
                }
            }

            for (int i = 0; i < parent2.RouteSize; i++)
            {
                City city = parent2.GetCityAtIndex(i);
                if (!child.ContainsCity(city))
                {
                    for (int j = 0; j < child.RouteSize; j++)
                    {
                        if (child.Route[j] == null)
                        {
                            child.SetCityAtIndex(j, city);
                            break;
                        }
                    }
                }
            }


            return child;
        }

        //private static void Mutate(Individual individual)
        //{
        //    // Loop through tour cities
        //    Random r = new Random();
        //    const double mutationPercent = ;

        //    if(r.Next())
        //}

        private static void Mutate(Population pop)
        {
            Random r = new Random();
            int mutationPercent = pop.PopulationSize / 10;

            for (int i = 0; i < mutationPercent; i++)
            {
                Individual toMutate = pop.GetInduvidualAtIndex(r.Next(0, pop.PopulationSize));

                int firstCityIndex = r.Next(0, toMutate.RouteSize);
                int secondCityIndex = r.Next(0, toMutate.RouteSize);

                City firstCity = toMutate.GetCityAtIndex(firstCityIndex);
                City secondCity = toMutate.GetCityAtIndex(secondCityIndex);

                toMutate.SetCityAtIndex(secondCityIndex, firstCity);
                toMutate.SetCityAtIndex(firstCityIndex, secondCity);

            }

        }

    }
}
