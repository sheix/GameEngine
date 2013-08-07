using System;
using Engine;

namespace SimpleGame
{
	public class ManualStrategy : IStrategy
	{
		public ManualStrategy ()
		{

		}
		#region IStrategy implementation
		IAct IStrategy.SelectAction (System.Collections.Generic.List<IAct> possibleActions, IScene scene)
		{
			Console.WriteLine ("Press Enter to start playing");
			ConsoleKeyInfo key;	
			key = Console.ReadKey();
			while (key.KeyChar < '0' || key.KeyChar > '9')
			{
				Console.Write("You have {0} possible actions select one (m) : ", possibleActions.Count);
				key = Console.ReadKey();
				if (key.KeyChar.Equals('m')){
					Console.WriteLine();
					var i = 0;
					foreach (var item in possibleActions) {
						Console.WriteLine(i +" : " + item.ToString());
						i++;
					}
				}

			}
			return possibleActions[int.Parse(key.KeyChar.ToString())];
		}
		#endregion

	}
}

