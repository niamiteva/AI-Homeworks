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

        //cities coordinates initialization
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

        //individual initialization
        public Individual(City[] route)
        {
            Route = ShuffleList(route);
            RouteSize = route.Length;
            DistanceFitness = 0;
        }

        public Individual(Individual parent)
        {
            RouteSize = parent.RouteSize;
            Route = new List<City>(parent.RouteSize);
            for (int i = 0; i < parent.RouteSize; i++)
            {
                Route.Add(null);
            }
        }

        private List<City> ShuffleList(City[] givenRoute)
        {
            List<City> randomList = new List<City>();
            List<City> route = new List<City>(givenRoute); 

            Random r = new Random();
            int randomIndex = 0;

            while (route.Count > 0)
            {
                randomIndex = r.Next(0, route.Count); 
                randomList.Add(route[randomIndex]); 
                route.RemoveAt(randomIndex); 
            }

            return randomList; 
        }

        public bool ContainsCity(City city)
        {
            return Route.Contains(city);
        }

        public int GetRouteFitness()
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

        public City GetCityAtIndex(int index)
        {
            return Route[index];
        }

        public void SetCityAtIndex(int index, City city)
        {
            Route[index] = city;
        }
        
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("|");

            for (int i = 0; i < RouteSize; i++)
            {
                str.Append(GetCityAtIndex(i).X.ToString() + "|" + GetCityAtIndex(i).Y.ToString() + "|");
            }

            return str.ToString();
        }
    }
}
