using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace Game
{
    /// <summary>
    /// There are 4 moons
    /// There are 4 seasons, each one of 90 days
    /// There are 3 months in each season
    /// 30 days are length of each month
    /// </summary>
    public class Calendar : ICalendar
    {
        private IScene _scene;
        public string SetMission { get; private set; }

        public object Today { get { return new Date(DayInYear, Year, GetMoonStates(Moons)); } }

        private List<MoonState> GetMoonStates(List<object> moons)
        {
            var result = new List<MoonState>();
            foreach (Moon moon in moons)
            {
                if (moon.Position == moon.Period)
                {
                    result.Add(MoonState.FullMoon);
                    continue;
                }

                if (moon.Position == moon.Period / 2 + 1)
                {
                    result.Add(MoonState.NoMoon);
                    continue;
                }
                result.Add(MoonState.None);
            }
            return result;
        }

        public const int StartYear = 1328;
        public const int DaysInYear = 360;



        public Calendar(IGame game, int day = 1)
        {
            game.KeyPressed += game_KeyPressed;
            DayFromStart = day;
            Year = StartYear + (day - 1) / DaysInYear;
            Moons = new List<object>{
                                                  new Moon("Lun",27),
                                                  new Moon("Mun",19),
                                                  new Moon("Sput", 37)
                                    };
        }

        void game_KeyPressed(object sender, EventArgs e)
        {
            var key = (e as KeyPressedEventArgs).Key;
            switch (key)
            {
                case "E": NextDay();
                    break;
                default:
                    int number;
                    if (int.TryParse(key[3].ToString() ,out number))
                        if (GetAvailableMissions().Count > number)
                            SetMission = GetAvailableMissions()[number];
                    break;
            }
                
        }

        public void AttachScene(IScene scene)
        {
            _scene = scene;
            scene.OnTick += scene_OnTick;
        }

        public void DetachScene()
        {
            _scene.OnTick -= scene_OnTick;
        }

        void scene_OnTick(object sender, EventArgs e)
        {
            SecondInDay++;
            if (SecondInDay == SecondsInDay)
            {
                SecondInDay = 1;
                NextDay();
            }
        }

        protected int SecondsInDay
        {
            get { return 86400; }
        }

        protected int SecondInDay { get; set; }

        public int DayFromStart { get; private set; }
        public int Year { get; private set; }

        public int DayInYear { get { return DayFromStart - (Year - StartYear) * DaysInYear; } }


        public List<object> Moons { get; private set; }

        public void NextDay()
        {
            Console.WriteLine("Next Day");
            DayFromStart++;
            if ((DayFromStart - 1) % DaysInYear == 0) Year++;
            foreach (Moon moon in Moons)
            {
                moon.NextDay();
            }
        }

        public List<string> GetAvailableMissions()
        {
            return new List<string> {"Default", "Home"};
        }

        public string Play()
        {
            while (true)
            {
                foreach (var nextMission in GetAvailableMissions())
                {
                    if (nextMission.Equals(SetMission))
                        return nextMission;
                }
                //Tick();
            }
        }
    }

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

    public class MoonState
    {
        public static MoonState None = new MoonState();
        public static MoonState FullMoon = new MoonState();
        public static MoonState NoMoon = new MoonState();

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

        public int Period { get { return _period; } }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void NextDay()
        {
            Position++;
            if (Position > _period) Position = 1;
        }
    }
}