using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class Population
    {
        public Individual[] Individuals { get; set; }
        public int PopulationSize { get;}

        public Population(int popSize)
        {
            Individuals = new Individual[popSize];
            PopulationSize = popSize;
        }

        public void InitialisePopulation(int numberOfCities)
        {
            Individual firstParent = new Individual(numberOfCities);

            for (int i = 0; i < PopulationSize; i++)
            {
                Individual newRoute = new Individual(firstParent.Route);
                Individuals[i] = newRoute;
            }
        }

        public Individual GetInduvidualAtIndex(int index)
        {
            return Individuals[index];
        }

        public void SetIndividualAtIndex(int index, Individual newIndividual)
        {
            Individuals[index] = newIndividual;
        }

        // Gets the best tour in the population
        public Individual GetFittest()
        {
            Individual fittest = Individuals[0];

            for (int i = 1; i < PopulationSize; i++)
            {
                if (fittest.DistanceFitness <= GetInduvidualAtIndex(i).DistanceFitness)
                {
                    fittest = GetInduvidualAtIndex(i);
                }
            }

            return fittest;
        }
    }
}
