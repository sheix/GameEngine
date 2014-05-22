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

        #region Implementation of IPlacableActor

        public int InitialX
        { get; set; }

        public int InitialY
        { get; set; }

        public event onMove Move;

        #endregion
    }
}
