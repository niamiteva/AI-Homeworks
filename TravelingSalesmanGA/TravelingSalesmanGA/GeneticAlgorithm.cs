using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class GeneticAlgorithm
    {
        private double mutationPercent = 0.015;
        private static int tournamentSize = 5;
        //public Random randomIndex = new Random();

        public static Population EvolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.PopulationSize);

            // Keep our best individual if elitism is enabled
            //int elitismOffset = 0;
            //if (elitism)
            //{
            //    newPopulation.saveTour(0, pop.getFittest());
            //    elitismOffset = 1;
            //}

            // Crossover population
            // Loop over the new population's size and create individuals from
            // Current population
            //for (int i = elitismOffset; i < newPopulation.populationSize(); i++)
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
            for (int i = 0; i < newPopulation.PopulationSize; i++)
            {
                Mutate(newPopulation.GetInduvidualAtIndex(i));
            }

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
            Individual child = new Individual();
            Random r = new Random();

            // Get start and end sub tour positions for parent1's tour
            int startCrossPoint = r.Next(0, parent1.RouteSize);
            int endCrossPoint = r.Next(0, parent1.RouteSize);
            
            for (int i = 0; i < parent1.RouteSize; i++)
            {
                // If our start position is less than the end position
                if (startCrossPoint < endCrossPoint && i > startCrossPoint && i < endCrossPoint)
                {
                    child.SetCityAtIndex(i, parent1.GetCityAtIndex(i));
                } // If our start position is larger
                else if (startCrossPoint > endCrossPoint)
                {
                    if (!(i < startCrossPoint && i > endCrossPoint))
                    {
                        child.SetCityAtIndex(i, parent1.GetCityAtIndex(i));
                    }
                }
            }

            // Loop through parent2's city tour
            for (int i = 0; i < parent2.RouteSize; i++)
            {
                // If child doesn't have the city add it
                if (!child.ContainsCity(parent2.GetCityAtIndex(i)))
                {
                    // Loop to find a spare position in the child's tour
                    for (int ii = 0; ii < child.RouteSize; ii++)
                    {
                        // Spare position found, add city
                        if (child.GetCityAtIndex(ii) == null)
                        {
                            child.SetCityAtIndex(ii, parent2.GetCityAtIndex(i));
                            break;
                        }
                    }
                }
            }
            return child;
        }

        private static void Mutate(Individual individual)
        {
            // Loop through tour cities
            Random r = new Random();
            for (int firstCirtyIndex= 0; firstCirtyIndex < individual.RouteSize; firstCirtyIndex++)
            {
                // Apply mutation rate
                //if (Math.random() < mutationRate)
                //{
                    // Get a second random position in the tour
                    int secondCityIndex = r.Next(0, individual.RouteSize);

                    // Get the cities at target position in tour
                    City firstCity = individual.GetCityAtIndex(firstCirtyIndex);
                    City secondCity = individual.GetCityAtIndex(secondCityIndex);

                    // Swap them around
                    individual.SetCityAtIndex(secondCityIndex, firstCity);
                    individual.SetCityAtIndex(firstCirtyIndex, secondCity);
                //}
            }
        }

    }
}
