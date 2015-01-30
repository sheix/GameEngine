using System;
using System.Linq;
using Engine.Contracts;
using System.Collections.Generic;
using Game;

namespace Game
{
	public class ManualStrategy : IStrategy
	{
	    private string LastAction;

		public ManualStrategy ()
		{
            
		}

		public void SubscribeToGame(IGame game)
		{
			game.KeyPressed += game_KeyPressed;
		}

        void game_KeyPressed(object sender, System.EventArgs e)
        {
            LastPressedKey((e as KeyPressedEventArgs).Key);
        }

		public IAct SelectAction (List<IAct> possibleActions, IScene scene)
		{
		    IAct result = null;
            
            while (result == null)
            {
                result = possibleActions.Where(act => act.Name == LastAction).FirstOrDefault();
            }

		    LastAction = null;
		    return result;
		}

	    public void LastPressedKey(string code)
	    {
			if (code.Equals ("p")) {
				LastAction = "Take";
				return;
			} 
            if (code.Equals("Numpad5") || code.Equals("Num5")) 
            {
                LastAction = "Wait";
                return;
            }
			// Works on Up Down Left Right - for moving
	        LastAction = code;
	    }
	}
}

