using System.Collections.Generic;
using Engine.Contracts;
using Engine;
using Game.Acts;

namespace Game
{
	public class ActorFactory : IActorFactory
	{
		private readonly IStrategy _manualStrategy;
		private static int _id = 0;
		private Player _player;

		public ActorFactory (IStrategy strategy)
		{
			_manualStrategy = strategy; 
		}

		private void AddMoveActions (IActor actor)
		{
			actor.AllActions.AddRange (new List<IAct> {new MoveAct("Up", actor),
				new MoveAct("Down", actor),
				new MoveAct("Left", actor),
				new MoveAct("Right", actor),
			});
		}

		private void AddAttackActions (IActor actor)
		{
			actor.AllActions.AddRange (new List<IAct> {
				new AttackAct("Up", actor),
				new AttackAct("Down", actor),
				new AttackAct("Left", actor),
				new AttackAct("Right", actor),
			});
		}

		private void AddWaitAction (IActor actor)
		{
			actor.AllActions.Add (new WaitAct (actor));
		}

		public IPlacableActor GetPlayer ()
		{
			if (_player == null) {
				_player = new Player (_manualStrategy);

				AddMoveActions (_player);
				AddAttackActions (_player);
				AddWaitAction (_player);
			}
			return _player;
		}

		public IPlacableActor GetActor ()
		{
			var actor = new PlacableActor ("Unit" + _id, new RandomStrategy (), 5, 5);
			AddMoveActions (actor);
			AddAttackActions (actor);
			AddWaitAction (actor);
			return actor;
		}
	}
}