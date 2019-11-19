using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class GeneticAlgorithm
    {

        private const int tournamentSize = 5;
        private const int mutationFactor = 10;

        public static Population EvolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.PopulationSize);
            
            for (int i = 0; i < newPopulation.PopulationSize; i++)
            {
                Individual parent1 = TournamentSelection(pop);
                Individual parent2 = TournamentSelection(pop);

                Individual child = Crossover(parent1, parent2);

                newPopulation.SetIndividualAtIndex(i, child);
            }
            
            Mutate(newPopulation);

            return newPopulation;
        }

        private static Individual TournamentSelection(Population pop)
        {
            Population tournament = new Population(tournamentSize);
            Random r = new Random();

            // For each place in the tournament get a random candidate tour and add it
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomId =r.Next(0, pop.PopulationSize);
                tournament.SetIndividualAtIndex(i, pop.GetInduvidualAtIndex(randomId));
            }
            //then get the fittest candidate in the tournament pop
            Individual fittest = tournament.GetFittest();

            return fittest;
        }

        public static Individual Crossover(Individual parent1, Individual parent2)
        {
            //two point crossover

            Individual child = new Individual(parent1);
            Random r = new Random();
            
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

        private static void Mutate(Population pop)
        {
            Random r = new Random();
            int mutationPercent = pop.PopulationSize / mutationFactor;

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
