using System;
using Engine;

namespace SimpleGame
{
	public class PlayerActor : Actor
	{
		public PlayerActor(IStrategy strategy) : base(strategy)
		{
			allActions = new System.Collections.Generic.List<IAct>
			                 {
			                     new Rock("Rock", this),
			                     new Scissors("Scissors", this),
			                     new Paper("Paper", this)
			                 };
		}

		public String selection; 

	}
}

