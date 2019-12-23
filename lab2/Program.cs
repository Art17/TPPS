using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Europe
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Program started.");
                if (calculateDays())
                    break;
            }
            Console.ReadKey();
            
        }

        private static bool calculateDays()
        {
            List<Testcase> testcases = new List<Testcase>();
            Console.Write("Write the number of countries: ");
            while (true)
            {
                string countStr = Console.ReadLine();
                int count;
                try
                {
                    count = Convert.ToInt32(countStr);
                }
                catch (FormatException err)
                {
                    Console.WriteLine("Error: countries count should be an integer");
                    return false;
                }

                if (count == 0)
                    break;
                else if (count < 1 || count > 20)
                {
                    Console.WriteLine("Error: there can be no more than 20 countries");
                    return false;
                }
                Console.WriteLine("Enter countries(name xl xy xh yh). Type 0 to finish");
                Testcase testcase = new Testcase();
                for (int i = count; i > 0; i--)
                {
                    string countryData = Console.ReadLine();
                    string[] countryArray = countryData.Split(' ');
                    if (countryArray.Length != 5)
                    {
                        i++;
                        Console.WriteLine("Bad country name and coordinates! Write it down again");
                        continue;
                    }
                    string countryName = countryArray[0];
                    if (countryName.Length > 25)
                    {
                        Console.WriteLine("Error: length of the country name is more than 25 characters");
                        return false;
                    }
                    int xl, yl, xh, yh;
                    try
                    {
                        xl = Convert.ToInt32(countryArray[1]);
                        yl = Convert.ToInt32(countryArray[2]);

                        xh = Convert.ToInt32(countryArray[3]);
                        yh = Convert.ToInt32(countryArray[4]);
                    }
                    catch (FormatException err)
                    {
                        Console.WriteLine("Error: coordinates aren't numbers");
                        return false;
                    }
                    if (xl < 1 || xl > 10
                        || yl < 1 || yl > 10 ||
                        xh < 1 || xh > 10 ||
                        yh < 1 || yh > 10)
                    {
                        Console.WriteLine("Error: every coordinate must be from 1 to 10");
                        return false;
                    }
                    testcase.addCountryName(new CountryName(countryName, xl, yl, xh, yh));
                }
                testcases.Add(testcase);
            }

            int caseNumber = 1;
            foreach (Testcase tc in testcases)
            {
                Console.WriteLine("Case Number " + caseNumber);
                tc.setCityNeighbours();
                if (tc.CountryNames.Count > 1 && !tc.checkCountriesConnection())
                {
                    return false;
                }
                tc.sortCountries();
                int days = 0;
                while (true)
                {
                    if (tc.checkCoinsDistribution(days))
                        break;
                    tc.distributeCoins();
                    days++;
                }
                caseNumber++;
            }

            return true;
        }
    }
}
