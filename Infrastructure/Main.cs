using System;
using Castle.Windsor;
using Castle;
using System.Collections.Generic;


namespace Infrastructure
{
	public class Application
	{
		public static void Main(string[]args)
		{
			//start the game!
			Console.WriteLine ("Launch host app!");
			//			Anaglyph a = new Anaglyph();
			//          a.Run(10.0);
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

