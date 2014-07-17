using System.Collections.Generic;
using Contracts;
using Engine;
using Game.Acts;

namespace Game
{
    public class ActorFactory : IActorFactory
    {
        private readonly ManualStrategy _strategy;
        private static int _id = 0;
        private Player _player;

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

        private void AddAttackActions(IActor actor)
        {
            actor.AllActions.AddRange(new List<IAct>{new AttackAct("Up", actor, null, null, null),
                                                        new AttackAct("Down", actor, null, null, null),
                                                        new AttackAct("Left", actor, null, null, null),
                                                        new AttackAct("Right", actor, null, null, null),
                                                       });
        }

        private void AddWaitAction(IActor actor)
        {
            actor.AllActions.Add(new WaitAct(actor));
        }

        public IPlacableActor GetPlayer()
        {
            if (_player == null)
            {
                _player = new Player(_strategy);

                AddMoveActions(_player);
                AddAttackActions(_player);
                AddWaitAction(_player);
            }
            return _player;
        }



        public IPlacableActor GetActor()
        {
            var actor = new PlacableActor("Unit" + _id, new RandomStrategy(), 5, 5);
            AddMoveActions(actor);
            AddAttackActions(actor);
            AddWaitAction(actor);
            return actor;
        }
    }
}