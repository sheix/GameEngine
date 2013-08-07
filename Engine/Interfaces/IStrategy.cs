using System;
using System.Collections.Generic;

namespace Engine
{
	/// <summary>
	/// Strategy : Possible implementations : 
	/// AI - random, determenined, adaptive
	/// Human player - waits for human input
	/// Network player
	/// </summary>
	public interface IStrategy
	{
		IAct SelectAction(List<IAct> possibleActions, IScene scene);
	}
}

