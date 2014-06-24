using System;
using System.Collections.Generic;


namespace Contracts
{
	public interface IScene
	{
		List<IActor> GetActors();
		void Tick();
		List<IAct> GetPossibleActions(IActor actor);
		void AddActor(IActor actor);
		void RemoveActor(IActor actor);
		string Play();
		string ID {get;}
	    event EventHandler MessageSent;
	    event EventHandler OnTick;
	}
}

