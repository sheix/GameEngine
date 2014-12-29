using System.Collections.Generic;

namespace Engine.Contracts
{
    public interface ISceneTemplate
    {
        List<IRule> GetRules();
        void AddRule(IRule rule);
    }
}