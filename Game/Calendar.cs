using System;
using System.Collections.Generic;
using Engine.Contracts;

namespace Game
{
    public class Calendar : ICalendar
    {
        private IScene _scene;

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



        public Calendar(int day = 1)
        {
            DayFromStart = day;
            Year = StartYear + (day - 1) / DaysInYear;
            Moons = new List<object>{
                                                  new Moon("Lun",27),
                                                  new Moon("Mun",19),
                                                  new Moon("Sput", 37)
                                    };
        }

        
        public void AttachScene(IScene scene)
        {
            _scene = scene;
            scene.OnTick += scene_OnTick;
        }

        public void DetachScene(IScene scene)
        {
            _scene = null;
            scene.OnTick -= scene_OnTick;
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

        
        public string Play()
        {
            Console.WriteLine("Calendar.Play");
//            while (true)
//            {
//                foreach (var nextMission in GetAvailableMissions())
//                {
//                    if (nextMission.Equals(SetMission))
//                        return nextMission;
//                }
//            }
			return "Default";
        }
    }
}