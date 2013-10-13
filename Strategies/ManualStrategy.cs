using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace OfficeRat.Strategies
{
    class ManualStrategy : IStrategy
    {
        public IAct SelectAction(List<IAct> possibleActions, IScene scene)
        {
            ConsoleKeyInfo key;	
			key = Console.ReadKey();
			while (key.KeyChar < '0' || key.KeyChar > '9')
			{
				key = Console.ReadKey(true);
				ShowAllPossibleActions(key, possibleActions);
            }
			return possibleActions[int.Parse(key.KeyChar.ToString())];
		}

        private void ShowAllPossibleActions(ConsoleKeyInfo key, IEnumerable<IAct> possibleActions)
        {
            if (key.KeyChar.Equals('?')){
                Console.WriteLine();
                var i = 0;
                foreach (var item in possibleActions) {
                    Console.WriteLine(i +" : " + item.ToString());
                    i++;
                }
            }
        }
    }
    
}
