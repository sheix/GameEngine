using System;
using System.Collections.Generic;
using Contracts;
using Engine;

namespace Game
{
    public class ActorFactory
    {
        private readonly ManualStrategy _strategy;

        public ActorFactory(ManualStrategy strategy)
        {
            _strategy = strategy;
            
        }

        private void AddMoveActions(IActor actor)
        {
            actor.AllActions.AddRange(new List<IAct>{new MoveAct("Up", actor, null, null, null),
                                                        new MoveAct("Down", actor, null, null, null),
                                                        new MoveAct("Left", actor, null, null, null),
                                                        new MoveAct("Right", actor, null, null, null),
                                                       });
        }

        public IPlacableActor GetPlayer()
        {
            var player = new Player(_strategy) {AllActions = new List<IAct>()};

            AddMoveActions(player);

            return player;
        }


    }
}
