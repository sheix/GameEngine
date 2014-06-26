using System;
using System.Collections.Generic;
using EngineContracts.Interfaces;

namespace Contracts
{
    public class Scenario
    {
        private ISceneFactory sceneFactory;
        private class SceneInfo
        {
            public IScene Scene;
            public ISceneTemplate Template;
            public bool IsGenerated;
        }

        private String ID;
        private Dictionary<string, SceneInfo> Scenes;
        
        public void RunScenario()
        {
            var scene = Scenes["Default"];

            while (true)
            {
                if (!scene.IsGenerated)
                {
                    scene.Scene = sceneFactory.Generate(scene.Template);
                }
                else
                {
                    sceneFactory.UpdateScene(scene.Scene, scene.Template);
                }
                //scene.Scene.Populate();
            }
        }
            

    }

    public interface ISceneTemplate
    {
        List<IRule> GetRules();
    }

    public interface IRule
    {
    }
}