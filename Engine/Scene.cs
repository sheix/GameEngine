using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace Engine
{
	public class Scene : IScene, IRenderable, IStage
    {
		private readonly List<IActor> _actors;
		private long _time = 0;
		private string _id;
		private IGrid _map;
		private Dictionary<Func<IScene , bool>, string> nextScenes;

		protected event EventHandler OnTick;

		public IGrid Map{get { return _map; }}

		public string ID{get {return _id;}}
		public delegate void EventHandler();

		public Scene(string id) 
		{
			_actors = new List<IActor>();
			id = _id;
		}

		public virtual void AddActor(IActor actor)
		{
			_actors.Add(actor);
			_actors.Sort((x,y) => x.GetInitiative() - y.GetInitiative());
		}

		public void PlaceActorToGrid(IPlacableActor actor)
		{
			Map.Grid[actor.InitialX][actor.InitialY].Actor = actor;
		}

		public void PlaceActorToGrid(IPlacableActor actor, Vector v)
		{
			Map.Grid[v._x][v._y].Actor = actor;
		}

		public virtual void RemoveActor (IActor actor)
		{
			if (actor is IPlacableActor)
			{
				var coords = _map.GetActorCoordinates(actor);
				_map.At(coords).Actor = null;
			}
			_actors.Remove(actor);
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
			while (true)
			{
				foreach (var transfer in nextScenes) {
				if (transfer.Key.Invoke(this))
					return transfer.Value;
				}
				Tick();
			}
			return "SceneEnded";
		}
		
	}
}

