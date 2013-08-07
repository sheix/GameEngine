using System;
using Engine;

namespace SimpleGame
{
	public class PlayerActor : Actor
	{
		public PlayerActor(IStrategy strategy) : base(strategy)
		{
			allActions = new System.Collections.Generic.List<IAct>();
			allActions.Add(new Rock("Rock",this));
			allActions.Add(new Scissors("Scissors",this));
			allActions.Add(new Paper("Paper", this));
		}

		public String selection; 

	}
}

