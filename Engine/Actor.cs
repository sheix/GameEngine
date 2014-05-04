using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace Engine
{
    public class Actor : IActor
    {
		private int _initiative;
		private List<IAct> _possibleActions;
		private readonly IStrategy _strategy;
		protected List<IAct> allActions;
		public string Name {get; set;}

		public Actor(IStrategy strategy)
		{
			_strategy = strategy;
		}
		public List<IAct> AllActions {get { return allActions;} set {allActions = value;}}

		public void Act(IScene scene)
		{
            _possibleActions = scene.GetPossibleActions(this);
			var action = _strategy.SelectAction(_possibleActions, scene);
			_initiative += action.Do(scene);
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
