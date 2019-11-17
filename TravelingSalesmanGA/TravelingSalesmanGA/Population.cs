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

        public void InitialiseFirstPopulation(int numberOfCities)
        {
            Individual firstParent = new Individual(numberOfCities);
            City[] parentRoute = new City[firstParent.RouteSize];
            firstParent.Route.CopyTo(parentRoute);

            for (int i = 0; i < PopulationSize; i++)
            {
                Individual newRoute = new Individual(parentRoute);
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
            fittest.GetRouteFitness();

            for (int i = 1; i < PopulationSize; i++)
            {
                if (fittest.GetRouteFitness() >= GetInduvidualAtIndex(i).GetRouteFitness())
                {
                    fittest = GetInduvidualAtIndex(i);
                }
            }

            return fittest;
        }
    }
}
