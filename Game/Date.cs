using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Date
    {
        public Date(int dayInYear, int year, List<MoonState> moonStates)
        {
            Day = dayInYear;
            Year = year;
            MoonStates = moonStates;
        }

        public int Year { get; private set; }
        public int Day { get; private set; }
        public List<MoonState> MoonStates { get; private set; }
        public bool SpecialDay { get { return MoonStates.Any(m => m != MoonState.None); } }
    }
}