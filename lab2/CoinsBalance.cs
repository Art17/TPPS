using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Europe
{
    public class CoinsBalance
    {
        private string countryName;
        public string CountryName
        {
            get { return countryName; }
        }
        private int coinsNumber;
        public int CoinsNumber
        {
            get { return coinsNumber; }
            set { coinsNumber = value; }
        }

        public CoinsBalance(string countryName, int initialCoinBalance)
        {
            this.countryName = countryName;
            this.coinsNumber = initialCoinBalance;
        }
    }
}
