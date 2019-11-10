using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class Individual
    {
        public List<City> Route { get; set; }
        public int DistanceFitness { get; set; }
        public int RouteSize { get; set; }

        public Individual()
        {
            Route = new List<City>();
        }

        public Individual(List<City> route)
        {
            Route = ShuffleList(route);
            RouteSize = route.Count;
            DistanceFitness = 0;
        }

        public Individual(int numberOfCities)
        {
            RouteSize = numberOfCities;
            Route = new List<City>();

            for (int i = 0; i < numberOfCities; i++)
            {
                Route.Add(new City(numberOfCities));
            }

            DistanceFitness = 0;
            DistanceFitness = GetRouteFitness();
        }

        private List<City> ShuffleList(List<City> inputList)
        {
            List<City> randomList = new List<City>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); 
                randomList.Add(inputList[randomIndex]); 
                inputList.RemoveAt(randomIndex); 
            }

            return randomList; 
        }

        private int GetRouteFitness()
        {
            if (DistanceFitness == 0)
            {
                int distance = 0;

                for (int i = 0; i < RouteSize; i++)
                {
                    City fromCity = Route[i];
                    City toCity;

                    if (i + 1 < RouteSize)
                    {
                        toCity = Route[i + 1];
                    }
                    else
                    {
                        toCity = Route[0];
                    }

                    distance += fromCity.GetDistanceBetweenTwoCities(toCity);
                }

                DistanceFitness = distance;
            }

            return DistanceFitness;
        }

        public bool ContainsCity(City city)
        {
            return Route.Contains(city);
        }

        public City GetCityAtIndex(int index)
        {
            return Route[index];
        }

        public void SetCityAtIndex(int index, City city)
        {
            Route[index] = city;
        }

        //public City GetCityAtIndex(int index)
        //TODO: setCityAtIndex(int index, city)
        //TODO: generateIndividual ???

        //TODO ToString()
    }
}
