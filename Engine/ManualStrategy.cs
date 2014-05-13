using System;
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
			throw new NotImplementedException ();
		}

		#endregion

	    public void LastPressedKey(string code)
	    {
	        LastAction = code;
	    }
	}
}

