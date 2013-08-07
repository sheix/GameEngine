using System;
using System.Collections.Generic;

namespace Engine
{
	public class Scene : IScene
	{
		private List<IActor> _actors;
		private long time = 0;
		private Func<IScene, bool> _end;
		protected event EventHandler OnTick;

		public delegate void EventHandler();

		public Scene() 
		{
			_actors = new List<IActor>();
		}

		public void AddActor(IActor actor)
		{
			_actors.Add(actor);
			_actors.Sort((x,y) => x.GetInitiative() - y.GetInitiative());
		}

		public void AddEndPredicate(Func<IScene, bool> end)
		{
			_end = end; 
		}

		#region IScene implementation
		public List<IActor> GetActors ()
		{
			return _actors;
		}

		public void Tick()
		{
			time ++;
			foreach (var actor in _actors) {
				actor.DecreaseInitiative();
			}

			foreach (var actor in _actors) {
				if (actor.GetInitiative() != 0) break;
				actor.Act(this);
			}

			if (OnTick != null)
				OnTick();
		}

		public virtual List<IAct> GetPossibleActions(IActor actor)
		{
			List<IAct> acts = new List<IAct>();
			foreach (var act in actor.AllActions) {
				if (act.CanDo(this))
					acts.Add(act);
			}
			return acts;
		}

		public string Play()
		{
			if ((_actors.Count) < 1) return "Error - Not enough actors";
			if (_end == null) return "Error - endless play";
			while (!_end(this))
				Tick();
			return "SceneEnded";
		}
		#endregion

	}
}

