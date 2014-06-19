using System;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// There are 4 moons
    /// There are 4 seasons, each one of 90 days
    /// There are 3 months in each season
    /// 30 days are length of each month
    /// </summary>
    public class Calendar
    {
        public const int StartYear = 1328;
        public const int DaysInYear = 360;

         

        public Calendar(int day = 1)
        {
            DayFromStart = day;
            Year = StartYear + (day-1) / DaysInYear;
            Moons = new List<Moon>{
                                                  new Moon("Lun",27),
                                                  new Moon("Mun",19),
                                                  new Moon("Sput", 37)
                                                  

    };
        }

        public int DayFromStart { get; private set; }
        public int Year { get; private set; }

        public int DayInYear { get { return DayFromStart - (Year - StartYear) * DaysInYear; } }

        
        public List<Moon> Moons
        {
            get; 
            private set;
        }

        public void NextDay()
        {
            DayFromStart++;
            if ((DayFromStart - 1) % DaysInYear == 0) Year++;
            foreach (var moon in Moons)
            {
                moon.NextDay();
            }
        }
    }

    public class Moon
    {
        private readonly string _name;
        private readonly int _period;


        public Moon(string name, int period)
        {
            _name = name;
            _period = period;
            Position = new Random().Next(1, _period + 1);
        }

        public int Position { get; private set; }

        public void NextDay()
        {
            Position++;
            if (Position > _period) Position = 1;
        }
    }
}