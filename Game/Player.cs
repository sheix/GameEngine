using Engine.Contracts;
using Engine;

namespace Game
{
    public class Player : AttributedActor
    {
        public Player(IStrategy strategy) : base("Player", strategy)
        {
            
        }
    }
}