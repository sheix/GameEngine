using System.Linq;
using Contracts;
using System.Collections.Generic;
using Game;

namespace Engine
{
	public class ManualStrategy : IStrategy
	{
	    private string LastAction;

		public ManualStrategy (IGame game)
		{
            game.KeyPressed += game_KeyPressed;
		}

        void game_KeyPressed(object sender, System.EventArgs e)
        {
            LastPressedKey((e as KeyPressedEventArgs).Key);
        }

		#region IStrategy implementation

		public IAct SelectAction (List<IAct> possibleActions, IScene scene)
		{
		    IAct result = null;
            
            while (result == null)
            { result = possibleActions.Where(act => act.Name == LastAction).FirstOrDefault(); }

		    LastAction = null;
		    return result;
		}

		#endregion

	    public void LastPressedKey(string code)
	    {
	        LastAction = code;
	    }
	}
}

