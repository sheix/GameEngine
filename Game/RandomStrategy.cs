using System;
using System.Collections.Generic;
using Engine.Contracts;

namespace Game
{
    public class RandomStrategy : IStrategy
    {
        public IAct SelectAction(List<IAct> possibleActions, IScene scene)
        {
            var r = new Random();
            return possibleActions[r.Next(possibleActions.Count)];
        }
    }
}