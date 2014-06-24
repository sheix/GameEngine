using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using EngineContracts;

namespace Engine
{
	public class Scene : IScene, IStage
    {
		private readonly List<IActor> _actors;
		private long _time = 0;
		private readonly string _id;
		private IGrid _map;
		private Dictionary<Func<IScene , bool>, string> nextScenes;

	    public event System.EventHandler OnTick;

		public IGrid Map{get { return _map; }
			set {_map = value;}
		}

		public string ID{get {return _id;}}

	    public event System.EventHandler MessageSent;

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

	    public Vector GetMapDimensions()
	    {
	        var vector = new Vector(_map.Grid.Count, _map.Grid[0].Count);
	        return vector;
	    }

	    public void Move(IPlacableActor self, string direction)
	    {
	        var location =  _map.GetActorCoordinates(self);
	        _map.At(location).Actor = null;
            
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
            _map.At(location).Actor = self;
	    }

	    public bool IsFreeInDirection(IPlacableActor actor, string direction)
	    {
            var location = _map.GetActorCoordinates(actor);
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
            if (_map.At(location).Actor != null) return false;
            if (_map.At(location) is Wall) return false;
	        return true;
	    }

        public void Attack(IPlacableActor self, string direction)
	    {
            // What should happen here?
	    }

	    public IActor ActorInDirection(IPlacableActor actor, string direction)
	    {
            var location = _map.GetActorCoordinates(actor);
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
	        return _map.At(location).Actor;
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
                string message = a.Act(this);
                if (MessageSent!= null)
                    MessageSent(this, new MessageEventArgs(message));
            }


		    if (OnTick != null)
				OnTick(this, null);
		    
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

        public void AddNextScene(string home, Func<IScene, bool> b)
	    {
            nextScenes.Add(b,home);
        }
    }
}

