using System;
using Engine;

namespace SimpleGame
{
	public class RandomStrategy : IStrategy
	{
		public RandomStrategy ()
		{
		}
		#region IStrategy implementation
		public IAct SelectAction (System.Collections.Generic.List<IAct> possibleActions, IScene scene)
		{
			return possibleActions[new Random(DateTime.Now.Millisecond).Next(possibleActions.Count)];
		}
		#endregion

	}
}

