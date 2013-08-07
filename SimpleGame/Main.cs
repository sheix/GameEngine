using System;
using Engine;
using System.Linq;

namespace SimpleGame
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine("Loading strategies");
			ManualStrategy manualStrategy = new ManualStrategy();
			RandomStrategy randomStrategy = new RandomStrategy();
			Console.WriteLine("Creating actors");
			IActor player = new PlayerActor(manualStrategy);
			player.Name = "Player";
			IActor enemy = new PlayerActor(randomStrategy);
			enemy.Name = "Computer";
			Console.WriteLine("Setting up scene");
			IScene scene = new SimpleGameScene();
			scene.AddActor(player);
			scene.AddActor(enemy);
			scene.AddEndPredicate(m => {
				var winnerList = (m as SimpleGameScene).winnerActor;
				if (winnerList.Count == 1)
					{
					Console.WriteLine ("Winner is: {0}", winnerList[0].Name);
					                   return true;
					                   }
					                   else return false;
					});

			Console.WriteLine("All set! Ready to play!");
			var outcome = scene.Play();

			Console.WriteLine("Scene finished with the followed outcome: "+ outcome);


		}
	}
}
