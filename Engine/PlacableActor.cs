using Contracts;

namespace Engine
{
    public class PlacableActor : Actor, IPlacableActor
    {
        public PlacableActor(string Name, IStrategy strategy) : base(Name, strategy)
        {
            InitialX = 10;
            InitialY = 10;
        }

        public PlacableActor(string Name, IStrategy strategy, int X, int Y)
            : base(Name, strategy)
        {
            InitialX = X;
            InitialY = Y;
        }
        #region Implementation of IPlacableActor

        public int InitialX
        { get; set; }

        public int InitialY
        { get; set; }

        public event onMove Move;

        #endregion
    }
}
