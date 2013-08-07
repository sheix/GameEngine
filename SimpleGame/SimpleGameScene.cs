using Engine;
using System;
using System.Collections.Generic;

namespace SimpleGame
{
	public class SimpleGameScene : Scene
	{
		public List<IActor> winnerActor {get; private set;}


		public SimpleGameScene()
		{
			winnerActor = new List<IActor>();
			OnTick += onTickEventHandler;
		}

		private void onTickEventHandler()
		{
			winnerActor = new List<IActor>();
			var allActors = this.GetActors();
			bool shouldCheck = true;
			foreach (var actor in allActors) {
				if (actor is PlayerActor)
					if (string.IsNullOrEmpty(((PlayerActor)actor).selection))
						shouldCheck = false;
			}
			if (shouldCheck)
			{
			for (int i=0; i<  allActors.Count-1;i++) {
					if (Draw(allActors[i],allActors[i+1]))
					{
						winnerActor = new List<IActor>();
						break;
					}
				if (Wins(allActors[i],allActors[i+1]))
						winnerActor.Add(allActors[i]);
					else
						winnerActor.Add(allActors[i+1]);
				}
			}
		}

		bool Draw (IActor iActor, IActor iActor2)
		{
			return ((PlayerActor)iActor).selection == ((PlayerActor)iActor2).selection;
		}		

		bool Wins (IActor iActor, IActor iActor2)
		{
			if (((PlayerActor)iActor).selection == "Scissors" && ((PlayerActor)iActor2).selection == "Paper")
				return true;
			if (((PlayerActor)iActor).selection == "Rock" && ((PlayerActor)iActor2).selection == "Scissors")
				return true;
			if (((PlayerActor)iActor).selection == "Paper" && ((PlayerActor)iActor2).selection == "Rock")
				return true;
			return false;
		}



	}
}

