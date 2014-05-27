using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace Engine
{
	public class Scene : IScene, IStage
    {
		private readonly List<IActor> _actors;
		private long _time = 0;
		private readonly string _id;
		private IGrid _map;
		private Dictionary<Func<IScene , bool>, string> nextScenes;

		protected event EventHandler OnTick;

		public IGrid Map{get { return _map; }
			set {_map = value;}
		}

		public string ID{get {return _id;}}
		public delegate void EventHandler();

		public Scene(string id) 
		{
			_actors = new List<IActor>();
			_id = id;
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

	    public Vector GetCenterOfInterest()
	    {
	        //default - player
	        return _map.GetActorCoordinates(_actors.Where(m => m.Name == "Player").FirstOrDefault());
	    }

	    public void Move(IPlacableActor self, string direction)
	    {
	        var location =  _map.GetActorCoordinates(self);
	        _map.At(location).Actor = null;
            Console.WriteLine("direction is: {0}",direction);
	        switch (direction)
	        {
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
            Console.WriteLine("New Location is: {0}",location);
            _map.At(location).Actor = self;
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

            foreach (var a in _actors.Where(a => a.GetInitiative() == 0))
            {
                a.Act(this);
            }


		    if (OnTick != null)
				OnTick();
		    
		}


	    public virtual List<IAct> GetPossibleActions(IActor actor)
		{
		    return actor.AllActions.Where(act => act.CanDo(actor,this)).ToList();
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

	    public void SetEmptyNextScene()
	    {
	        nextScenes = new Dictionary<Func<IScene, bool>, string> {{m => false, "Default"}};
	    }
    }
}

