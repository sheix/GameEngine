using System.Linq;
using Contracts;
using System.Collections.Generic;

namespace Engine
{
	public class ManualStrategy : IStrategy
	{
	    private string LastAction;

		public ManualStrategy ()
		{
		}

		#region IStrategy implementation

		public IAct SelectAction (List<IAct> possibleActions, IScene scene)
		{
		    IAct result = null;
            result = possibleActions.Where(act => act.Name == LastAction).FirstOrDefault();
		    
		    return result;
		}

		#endregion

	    public void LastPressedKey(string code)
	    {
	        LastAction = code;
	    }
	}
}

