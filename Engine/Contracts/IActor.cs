using System;
using System.Collections.Generic;

namespace Engine.Contracts
{
	public interface IActor
	{
		string Act(IScene scene);
		int GetInitiative();
		void DecreaseInitiative();
		string Name {get; set;}
		List<IAct> AllActions { get ; set;}
	    
	}
}

