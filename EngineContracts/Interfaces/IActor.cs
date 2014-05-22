using System;
using System.Collections.Generic;

namespace Contracts
{
	public interface IActor
	{
		void Act(IScene scene);
		int GetInitiative();
		void DecreaseInitiative();
		string Name {get; set;}
		List<IAct> AllActions { get ; set;}
	    
	}
}

