using System.Collections.Generic;

namespace Engine.Contracts
{
	public interface IScenarioLoader
	{
		Dictionary <string, IScene> Load (string path = "");
    }
}

