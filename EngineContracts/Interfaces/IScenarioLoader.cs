using System;
using System.Collections.Generic;

namespace Contracts
{
	public interface IScenarioLoader
	{
		Dictionary <string, IScene> Load (string path = "");
	}

    public class Scenario
    {
        private String ID;
        private IScene scene;
        
    }
}

