using Contracts;
using Engine;

namespace Game
{
    public class Player : PlacableActor
    {
        public Player(IStrategy strategy) : base("Player", strategy)
        {
            
        }
    }
}