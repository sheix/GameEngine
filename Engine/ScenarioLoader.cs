using System.Collections.Generic;
using Contracts;
using Engine;

namespace Engines
{
    class ScenarioLoader : IScenarioLoader
    {
        #region Implementation of IScenarioLoader

        public Dictionary<string, IScene> Load(string path)
        {
            var result = new Dictionary<string, IScene>();

            //read file
            //generate scenes
            result.Add("Default", new Scene("Default"));

            return result;
        }

        #endregion
    }
}
