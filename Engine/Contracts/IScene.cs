using System;
using System.Collections.Generic;


namespace Engine.Contracts
{
	public interface IScene : IPlayable, IStage 
	{
		List<IActor> GetActors();
		void Tick();
		List<IAct> GetPossibleActions(IActor actor);
		void AddActor(IActor actor);
		void RemoveActor(IActor actor);
		event EventHandler MessageSent;
	    event EventHandler OnTick;
	    void AddNextScene(string home, Func<IScene, bool> b);



	}

    public interface IPlayable
    {
        string Play();
    }
}

