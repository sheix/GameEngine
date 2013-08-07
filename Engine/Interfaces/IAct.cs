using System;

namespace Engine
{
	public interface IAct
	{
		bool CanDo(IScene scene);
		void Do(IScene scene);

	}
}

