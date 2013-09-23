using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Interfaces;

namespace Engine
{
    public class Scene : IScene, IRenderable
    {
		private readonly List<IActor> _actors;
		private long _time = 0;
		private Func<IScene, bool> _end;
		protected event EventHandler OnTick;

		public delegate void EventHandler();

		public Scene() 
		{
			_actors = new List<IActor>();
		}

		public virtual void AddActor(IActor actor)
		{
			_actors.Add(actor);
			_actors.Sort((x,y) => x.GetInitiative() - y.GetInitiative());
		}

		public virtual void RemoveActor (IActor actor)
		{
			_actors.Remove(actor);
		}

		public void AddEndPredicate(Func<IScene, bool> end)
		{
			_end = end; 
		}

		public List<IActor> GetActors ()
		{
			return _actors;
		}

		public void Tick()
		{
			_time ++;
			foreach (var actor in _actors) {
				actor.DecreaseInitiative();
			}

			foreach (var actor in _actors) {
				if (actor.GetInitiative() != 0) break;
				actor.Act(this);
			}

			if (OnTick != null)
				OnTick();
		    Render();
		}

        public virtual void Render()
	    {
	        
	    }

	    public virtual List<IAct> GetPossibleActions(IActor actor)
		{
		    return actor.AllActions.Where(act => act.CanDo(this)).ToList();
		}

		public string Play()
		{
			if ((_actors.Count) < 1) return "Error - Not enough actors";
			if (_end == null) return "Error - endless play";
			while (!_end(this))
				Tick();
			return "SceneEnded";
		}
		
	}
}

