using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Europe
{
    public class Testcase
    {
        private List<CountryName> countryNames;
        public List<CountryName> CountryNames
        {
            get { return countryNames; }
        }
        private const int NumberOfNeighbours = 4;

        public Testcase()
        {
            countryNames = new List<CountryName>();
        }

        public void addCountryName(CountryName country)
        {
            countryNames.Add(country);
        }

        public void setCityNeighbours()
        {
            foreach (CountryName currentCountry in this.countryNames)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    if (currentCity.getNeighboursCount() == NumberOfNeighbours)
                        continue;
                    foreach (CountryName anotherCountry in this.countryNames)
                    {
                        foreach (City anotherCity in anotherCountry.Cities)
                        {
                            if (currentCity.Equals(anotherCity))
                                continue;
                            if ((currentCity.X + 1 == anotherCity.X && currentCity.Y == anotherCity.Y) ||
                                (currentCity.X - 1 == anotherCity.X && currentCity.Y == anotherCity.Y) ||
                                (currentCity.X == anotherCity.X && currentCity.Y + 1 == anotherCity.Y) ||
                                (currentCity.X == anotherCity.X && currentCity.Y - 1 == anotherCity.Y))
                                currentCity.addNeighbour(anotherCity);
                        }
                    }
                }
            }
        }

        public bool checkCountriesConnection()
        {
            foreach (CountryName currentCountry in this.countryNames)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    foreach (City neighbour in currentCity.Neighbours)
                    {
                        if (String.Compare(currentCity.CountryName, neighbour.CountryName) != 0)
                        {
                            currentCountry.linkedFlag = true;
                            break;
                        }
                    }
                    if (currentCountry.linkedFlag)
                        break;
                }
                if (!currentCountry.linkedFlag)
                {
                    Console.WriteLine("Bad configuration of the countries. There's no connection of one of the commonwealth to the others");
                    return false;
                }
            }
            return true;
        }

        public void distributeCoins()
        {
            foreach (CountryName currentCountry in this.countryNames)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    currentCity.distributeCoinsToNeighbours();
                }
            }

            foreach (CountryName currentCountry in this.countryNames)
            {
                foreach (City currentCity in currentCountry.Cities)
                {
                    currentCity.mergeCoins();
                }
            }
        }

        public void sortCountries()
        {
            CountryNameComparer cc = new CountryNameComparer();

            this.countryNames.Sort(cc);
        }

        public bool checkCoinsDistribution(int days)
        {
            bool ret = true;
            foreach (CountryName currentCountry in this.countryNames)
            {
                int completeCityCount = 0;
                foreach (City currentCity in currentCountry.Cities)
                {
                    if(currentCity.CoinsBalances.Count == countryNames.Count)
                    {
                        completeCityCount++;
                    }
                }
                if (completeCityCount == currentCountry.Cities.Count && !currentCountry.completeFlag)
                {
                    currentCountry.completeFlag = true;
                    Console.WriteLine(currentCountry.Name + " " + days);
                }
                if (completeCityCount != currentCountry.Cities.Count)
                    ret = false;
            }
            return ret;
        }
    }

    public class CountryNameComparer : IComparer<CountryName>
    {
        public int Compare(CountryName x, CountryName y)
        {
            return String.Compare(x.Name, y.Name);
        }
    }
}
