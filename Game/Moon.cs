using System;

namespace Game
{
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