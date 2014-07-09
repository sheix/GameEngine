using System.Collections.Generic;

namespace Contracts
{
    public interface ISceneTemplate
    {
        List<IRule> GetRules();
    }
}