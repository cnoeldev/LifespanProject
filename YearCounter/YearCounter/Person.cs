using System;
using System.IO;

namespace YearCounter
{
    //A class for generation of people consisting of a first name, last name, and date of birth/death between 1900-2000.
    class Person
    {
        string _name;
        int[] _lifespan;

        //getters for name and lifespan
        public string Name
        {
            get { return _name; }
        }
        public int Birth {
            get { return _lifespan[0]; }
        }
        public int Death {
            get { return _lifespan[1]; }
        }

        public Person()
        {
            _name = Generator.MakeName();
            _lifespan = Generator.MakeLifespan();
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Name, Birth, Death);
        }
    }

    static class Generator
    {
        //Lists of names obtained from www2.census.gov
        static string[] firstNames = File.ReadAllLines(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Resources\FirstNames.txt");
        static string[] lastNames = File.ReadAllLines(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Resources\LastNames.txt");

        static int minYear = 1900;
        static int maxYear = 2000;

        public static string MakeName()
        {
            Random random = new Random();
            //select random first name from file
            int randomLineNumber = random.Next(0, firstNames.Length);
            string first = firstNames[randomLineNumber].Trim();
            //select random last name from file
            randomLineNumber = random.Next(0, lastNames.Length);
            string last = lastNames[randomLineNumber].Trim();

            //build name sting
            string name = first + ',' + last;

            return name;
        }

        public static int[] MakeLifespan()
        {
            //create random birth years from 1900 to 2000
            Random random = new Random();
            //adding 1 because random is exclusive ending, want to possibly get some with year 2000.
            int birth = random.Next(minYear, maxYear + 1);
            int death = random.Next(birth, maxYear + 1);

            //build array representing lifespan
            int[] lifespan = { birth, death };

            return lifespan;
        }
    }
}
