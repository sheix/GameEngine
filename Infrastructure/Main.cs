using System;
using Castle.Windsor;
using Castle;

namespace Infrastructure
{
	public class Application
	{
		public static void Main(string[]args)
		{
			//start the game!
			Console.WriteLine ("Launch host app!");
			container.Dispose();
		}

		public static IWindsorContainer container;

		static Application()
		{
			container = new WindsorContainer();
			//container.Register();
		}
	}
}

