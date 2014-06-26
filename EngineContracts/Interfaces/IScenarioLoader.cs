using System.Collections.Generic;

namespace Contracts
{
	public interface IScenarioLoader
	{
		Dictionary <string, IScene> Load (string path = "");
	}
}

