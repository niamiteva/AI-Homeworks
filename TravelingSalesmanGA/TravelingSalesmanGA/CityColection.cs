using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class CityColection
    {
        public List<City> Cities { get; set; }
        public int NumberOfCities { get; set; }

        public CityColection(int numberOfCities)
        {
            NumberOfCities = numberOfCities;
            Cities = new List<City>();

            for (int i = 0; i < numberOfCities; i++)
            {
                Cities.Add(new City(numberOfCities));
            }
            
        }
    }
}
