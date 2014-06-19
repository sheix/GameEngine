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

        private static List<Moon> _moons = new List<Moon>{
                                                  new Moon("Lun",27),
                                                  new Moon("Mun",19),
                                                  new Moon("Sput", 37)
                                                  

    };

        public Calendar(int day = 1)
        {
            DayFromStart = day;
            Year = StartYear + (day-1) / DaysInYear;
        }

        public int DayFromStart { get; private set; }
        public int Year { get; private set; }

        public int DayInYear { get { return DayFromStart - (Year - StartYear) * DaysInYear; } }

        public void NextDay()
        {
            DayFromStart++;
            if ((DayFromStart - 1) % DaysInYear == 0) Year++;
        }
    }

    internal class Moon
    {
        private readonly string _name;
        private int _period;

        public Moon(string name, int period)
        {
            _name = name;
            _period = period;
        }
    }
}