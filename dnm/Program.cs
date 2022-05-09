using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnm
{
    class Program
    {
        static void Main(string[] args)
        {
            MineDeneme();
            string a = "15";
            int b = Convert.ToInt32(a);
            if (b == 15)
            {
                string c = b.ToString();
                Console.WriteLine();
            }
            Console.Read();
        }

        private static void MineDeneme()
        {
            int row = 16;
            int column = 30;
            int sizeMine = 99;
            bool flagLocationMine = false;
            int[,] locationMine = new int[sizeMine, 2];
            Random random = new Random();
            for (int i = 0; i < sizeMine; i++)
            {
                //for (int j = 0; j < 2; j++)
                //{
                locationMine[i, 0] = random.Next(row);
                do
                {
                    flagLocationMine = false;
                    locationMine[i, 1] = random.Next(column);
                    //if (i != 0)
                    //{
                    for (int j = 0; j < i; j++)
                    {
                        if (locationMine[i, 0] == locationMine[j, 0] && locationMine[i, 1] == locationMine[j, 1])
                        {
                            flagLocationMine = true;
                        }
                    }
                    //}
                } while (flagLocationMine);
                //}
            }

            for (int i = 0; i < 99; i++)
            {
                Console.WriteLine("{0}  {1}", locationMine[i, 0].ToString(), locationMine[i, 1].ToString());
            }

            Console.Read();
        }
    }
}
