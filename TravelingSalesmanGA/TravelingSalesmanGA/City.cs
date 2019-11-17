using System;
using System.Collections.Generic;
using System.Text;

namespace TravelingSalesmanGA
{
    class City
    {
        public int X { get; set; }
        public int Y { get; set; }

        public City(int numberOfCities)
        {
            Random randomIndex = new Random();
            X = randomIndex.Next(0, numberOfCities * 10);
            Y = randomIndex.Next(0, numberOfCities * 10);
        }

        public City(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int GetDistanceBetweenTwoCities(City city)
        {
            //Pitagor's theorem => c^2 = a^2 + b^2 => distance = c^(1/2)
            int a = Math.Abs(X - city.X);
            int b = Math.Abs(Y - city.Y);

            return Convert.ToInt32(Math.Sqrt((a * a) + (b * b)));
        }
    }
}
