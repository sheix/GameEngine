using System;

namespace Contracts
{
	public interface IScenarioRunner
	{
		void LoadScenario();
		void Run(string sceneId = null);
	}
}

