using System;
using System.Collections.Generic;
using Engine.Contracts;

namespace Engine
{
    public class Actor : IActor
    {
		private int _initiative;
		private List<IAct> _possibleActions;
		private readonly IStrategy _strategy;
		protected List<IAct> allActions;
		public string Name {get; set;}

		public Actor(String name, IStrategy strategy)
		{
			Name = name;
			_strategy = strategy;
            AllActions = new List<IAct>();
		}
		public List<IAct> AllActions {get { return allActions;} set {allActions = value;}}

		public string Act(IScene scene)
		{
            _possibleActions = scene.GetPossibleActions(this);
			var action = _strategy.SelectAction(_possibleActions, scene);
            var result = action.Do(scene);
		    _initiative += result.TimePassed;
		    return result.Message;
		}

		public int GetInitiative()
		{
			return _initiative;
		}

		public void DecreaseInitiative()
		{
			if (_initiative == 0) return;
			_initiative --;
		}
    }
}
