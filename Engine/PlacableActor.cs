using Contracts;

namespace Engine
{
    public class PlacableActor : Actor, IPlacableActor
    {
        public PlacableActor(string name, IStrategy strategy) : base(name, strategy)
        {
            InitialX = 10;
            InitialY = 10;
        }

        public PlacableActor(string name, IStrategy strategy, int x, int y)
            : base(name, strategy)
        {
            InitialX = x;
            InitialY = y;
        }
        #region Implementation of IPlacableActor

        public int InitialX
        { get; set; }

        public int InitialY
        { get; set; }

        public event onMove Move;

        #endregion
    }

    public class AttributedActor : PlacableActor
    {
        public AttributedActor(string name, IStrategy strategy) : base(name, strategy)
        {
        }

        public AttributedActor(string name, IStrategy strategy, int x, int y) : base(name, strategy, x, y)
        {
        }

        public int Strength { get; private set; }
        public int Toughness { get; private set; }
        public int Dexterity { get; private set; }
        public int Accuracy { get; private set; }
        public int Learning { get; private set; }
        public int Willpower { get; private set; }
    }
} 
