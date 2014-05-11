using System;
using Contracts;
using System.Collections.Generic;

namespace Engine
{
	public class ManualStrategy : IStrategy
	{
		public ManualStrategy ()
		{
		}

		#region IStrategy implementation

		public IAct SelectAction (List<IAct> possibleActions, IScene scene)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

