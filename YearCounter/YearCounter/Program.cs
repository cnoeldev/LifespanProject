using System;
using System.IO;

namespace YearCounter
{
    class Program
    {
        static string _defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Resources\People.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Computing Result");

            //This call was used to build the names and lifespans for use in selecting the most populated year(s)
            BuildInitialFile(1000);

            int result = GetMostLivingYear(_defaultPath);

            Console.WriteLine(String.Format("The latest year with the most living people, exluding possible ties, is {0}", result));

            Console.Write("\nFinished. Press any key to exit.");
            Console.ReadKey();
        }

        private static int GetMostLivingYear(string dataPath)
        {
            //living year stores the counts of people living in each year, 0-100 will represent 1900 to 2000
            int[] livingYear = new int[101];
            //using offset value to convert from 1900-2000 down to 0-100;
            int offset = 1900;
            //highest year store the position in livingYear currently holding the most living people.
                //The case where multiple years have the same amount is considered trivial in this scenario.
            int highestVal = 0;
            int highestYear = 0;

            //pull all lines from file, readalllines because readonly purpose
            var lines = File.ReadAllLines(dataPath);
            foreach(string l in lines)
            {
                //using split to separate names from dates.  Vaguely simulating handling a formatted but natural dataset.
                string[] words = l.Split(' ');
                //no-try parsing words, as we know from the file where the years are and that there can be no invalid numbers.
                int start = int.Parse(words[1]) - offset;
                int end = int.Parse(words[2]) - offset;
                //iterating living year based on lifespan, to add a living count to each year properly.
                for(int i = start; i <= end; i++)
                {
                    livingYear[i]++;
                    //track the year with the most living people
                    if(livingYear[i] > highestVal)
                    {
                        highestVal = livingYear[i];
                        highestYear = i;
                    }
                }
            }

            //Add the offset back into the highest year so the result will be an actual year.
            highestYear += offset;

            foreach(int count in livingYear)
            {
                Console.WriteLine(String.Format("{0} living: {1}", offset, count));
                //won't need offset anymore, so reuse it to increment for printing.
                offset++;
            }

            return highestYear;
        }

        //Use the Person class to populate People.txt with a set of people and lifespans.  Size determines the number of people/lines.
        private static void BuildInitialFile(int size)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(_defaultPath);
                for (int i = 0; i < size; i++)
                {
                    Person p = new Person();
                    string line = p.ToString();
                    streamWriter.WriteLine(line);
                }
                streamWriter.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Initial File Exception: " + e.Message);
            }
        }
    }
}
