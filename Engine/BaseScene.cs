using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Contracts;

namespace Engine
{
	public class BaseScene : IScene, IStage
	{

		public string Name { get; private set; }

		public BaseScene (String name)
		{
			Name = name;
			_actors = new List<IActor> ();
			SetEmptyNextScene ();
		}

		private readonly List<IActor> _actors;
		private long _time = 0;
		private Dictionary<Func<IScene , bool>, string> nextScenes;

		public event System.EventHandler OnTick;

		protected IGrid Map{ get ; set; }

		public event System.EventHandler MessageSent;

		public delegate void EventHandler ();

		public virtual void AddActor (IActor actor)
		{
			_actors.Add (actor);
			_actors.Sort ((x,y) => x.GetInitiative () - y.GetInitiative ());
		}

		public void PlaceActorToGrid (IPlacableActor actor)
		{
			Map.Grid [actor.InitialX] [actor.InitialY].Actor = actor;
		}

		public void PlaceActorToGrid (IPlacableActor actor, Vector v)
		{
			Map.Grid [v._x] [v._y].Actor = actor;
		}

		public Vector GetCenterOfInterest ()
		{
			try {
				return Map.GetActorCoordinates (_actors.Where (m => m.Name == "Player").FirstOrDefault ());
			} catch (Exception e) {
				return new Vector (10, 10);
			}
		}

		public Vector GetMapDimensions ()
		{
			var vector = new Vector (Map.Grid.Count, Map.Grid [0].Count);
			return vector;
		}

		public void Move (IPlacableActor self, string direction)
		{
			var location = Map.GetActorCoordinates (self);
			Map.At (location).Actor = null;
            
			switch (direction) {
			case "Up":
				location._y--;
				break;
			case "Down":
				location._y++;
				break;
			case "Left":
				location._x--;
				break;
			case "Right":
				location._x++;
				break;
                    
			}
			Map.At (location).Actor = self;
		}

		public bool IsFreeInDirection (IPlacableActor actor, string direction)
		{
			var location = Map.GetActorCoordinates (actor);
			switch (direction) {
			case "Up":
				location._y--;
				break;
			case "Down":
				location._y++;
				break;
			case "Left":
				location._x--;
				break;
			case "Right":
				location._x++;
				break;
			}
			if (Map.At (location).Actor != null)
				return false;
			if (!Map.At (location).IsPassable ())
				return false;
			return true;
		}

		public void Attack (IPlacableActor self, string direction)
		{
			// What should happen here?
		}

		public IActor ActorInDirection (IPlacableActor actor, string direction)
		{
			var location = Map.GetActorCoordinates (actor);
			switch (direction) {
			case "Up":
				location._y--;
				break;
			case "Down":
				location._y++;
				break;
			case "Left":
				location._x--;
				break;
			case "Right":
				location._x++;
				break;
			}
			return Map.At (location).Actor;
		}

		public virtual void RemoveActor (IActor actor)
		{
			if (!_actors.Contains (actor))
				return;
			if (actor is IPlacableActor) {
				var coords = Map.GetActorCoordinates (actor);
				Map.At (coords).Actor = null;
			}
			_actors.Remove (actor);
		}

		public List<IActor> GetActors ()
		{
			return _actors;
		}

		public void Tick ()
		{
			_time ++;
			foreach (var actor in _actors) {
				actor.DecreaseInitiative ();
			}

			foreach (var a in _actors.Where(a => a.GetInitiative() == 0)) {
				string message = a.Act (this);
				if (MessageSent != null)
					MessageSent (this, new MessageEventArgs (message));
			}

			if (OnTick != null)
				OnTick (this, null);
		    
		}

		public virtual List<IAct> GetPossibleActions (IActor actor)
		{
			return actor.AllActions.Where (act => act.CanDo (actor, this)).ToList ();
		}

		public string Play ()
		{
			while (true) {
				foreach (var transfer in nextScenes) {
					if (transfer.Key.Invoke (this))
						return transfer.Value;
				}
				Tick ();
			}
		}

		private void SetEmptyNextScene ()
		{
			nextScenes = new Dictionary<Func<IScene, bool>, string> { {m => false, "Default"} };
		}

		public void AddNextScene (string home, Func<IScene, bool> b)
		{
			nextScenes.Add (b, home);
		}

		public void SetMap (IGrid grid)
		{
			Map = grid;
		}

		public ICell At (int x, int y)
		{
			return Map.At (x, y);
		}

		public bool HaveItemsBeneath (IActor actor)
		{
			var coordinates = Map.GetActorCoordinates (actor);
			return (Map.At (coordinates).Items.Count > 0);
		}
	}
}

