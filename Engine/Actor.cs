using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Actor : IActor
    {
		private int initiative;
		private List<IAct> PossibleActions;
		private IStrategy _strategy;
		protected List<IAct> allActions;
		public string Name {get; set;}

		public Actor(IStrategy strategy)
		{
			_strategy = strategy;
		}
		public List<IAct> AllActions {get { return allActions;} set {allActions = value;}}

		public void Act(IScene scene)
		{
			PossibleActions = scene.GetPossibleActions(this);
			var action = _strategy.SelectAction(PossibleActions, scene);
			action.Do(scene);
		}

		public int GetInitiative()
		{
			return initiative;
		}

		public void DecreaseInitiative()
		{
			if (initiative == 0) return;
			initiative --;

		}



    }
}
