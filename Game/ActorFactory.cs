using System;
using System.Collections.Generic;
using Contracts;
using Engine;

namespace Game
{
    public class ActorFactory
    {
        private readonly ManualStrategy _strategy;
        private static int _id = 0;

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
            var player = new Player(_strategy);

            AddMoveActions(player);

            return player;
        }

        public IPlacableActor GetActor()
        {
            var actor = new PlacableActor("Unit" + _id, new RandomStrategy(),5,5);
            AddMoveActions(actor);
            return actor;
        }


    }

    public class RandomStrategy : IStrategy
    {
        public IAct SelectAction(List<IAct> possibleActions, IScene scene)
        {
            var r = new Random();
            return possibleActions[r.Next(possibleActions.Count)];
        }
    }
}
