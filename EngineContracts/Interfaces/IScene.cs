using System;
using System.Collections.Generic;


namespace Contracts
{
	public interface IScene : IPlayable 
	{
		List<IActor> GetActors();
		void Tick();
		List<IAct> GetPossibleActions(IActor actor);
		void AddActor(IActor actor);
		void RemoveActor(IActor actor);
		event EventHandler MessageSent;
	    event EventHandler OnTick;
	}

    public interface IPlayable
    {
        string Play();
    }
}

