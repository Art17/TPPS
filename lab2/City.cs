using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Europe
{
    public class City
    {
        private int x;
        private int y;

        private List<CoinsBalance> coinsBalances;

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

        public List<CoinsBalance> CoinsBalances
        {
            get { return coinsBalances; }
        }
        private List<CoinsBalance> newCoinsBalances; 

        private List<City> neighbours;
        public List<City> Neighbours
        {
            get { return neighbours; }
        }

        private string countryName;
        public string CountryName
        {
            get { return countryName; }
        }

        private const int InitialCityCoinBalance = 1000000;
        private const int RepresentativePortion = 1000;

        public City(int xCoord, int yCoord, string countryName)
        {
            this.x = xCoord;
            this.y = yCoord;

            this.countryName = countryName;
            CoinsBalance thisCountryCoinsBalance = new CoinsBalance(countryName, InitialCityCoinBalance);
            coinsBalances = new List<CoinsBalance>();
            newCoinsBalances = new List<CoinsBalance>();
            neighbours = new List<City>();
            coinsBalances.Add(thisCountryCoinsBalance);
        }

        public void addNeighbour(City city)
        {
            neighbours.Add(city);
        }

        public int getNeighboursCount()
        {
            return neighbours.Count;
        }

        public void distributeCoinsToNeighbours()
        {
            foreach (CoinsBalance cb in coinsBalances)
            {
                int numberOfCoins = (int)(Math.Floor(cb.CoinsNumber / (float)RepresentativePortion));
                if (numberOfCoins == 0)
                    continue;

                foreach (City neighbour in this.neighbours)
                {
                    neighbour.addCoin(new CoinsBalance(cb.CountryName, numberOfCoins));
                    cb.CoinsNumber -= numberOfCoins;
                }
            }
        }

        public void addCoin(CoinsBalance newCoinsBalance)
        {
            newCoinsBalances.Add(newCoinsBalance);
        }

        public void mergeCoins()
        {
            bool isMatch;
            foreach (CoinsBalance ncb in newCoinsBalances)
            {
                isMatch = false;
                foreach (CoinsBalance cb in coinsBalances)
                {
                    if (cb.CountryName.Equals(ncb.CountryName))
                    {
                        cb.CoinsNumber += ncb.CoinsNumber;
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                    coinsBalances.Add(ncb);
            }

            newCoinsBalances.Clear();
        }
    }
}
