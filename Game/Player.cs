using System.Collections.Generic;
using Contracts;
using Engine;

namespace Game
{
    public class Player : PlacableActor
    {
        public Player(IStrategy strategy) : base("Player", strategy)
        {
            AllActions = new List<IAct>(new List<IAct> {new MoveAct("Up", this, null, null, null),
                                                        new MoveAct("Down", this, null, null, null),
                                                        new MoveAct("Left", this, null, null, null),
                                                        new MoveAct("Right", this, null, null, null),
                                                       });
        }

        
    }
}