using NUnit.Framework;
using System;
using Game.Acts;
using Engine.Contracts;
using Moq;
using Engine;
using System.Collections.Generic;

namespace GameTest
{
	[TestFixture()]
	public class TakeActShould
	{
		private Mock<IScene> scene;
		private Mock<IActor> actor;

		[Test()]
		public void ReturnTrueCanDoWhenThereIsAnItemBeneathTheActor ()
		{
			scene = new Moq.Mock<IScene> ();
			actor = new Mock<IActor> ();

			scene.Setup (mn => mn.HaveItemsBeneath (It.Is<IActor> (s => s.Equals(actor.Object)))).Returns (true); 

			IAct act = new TakeAct ();

			Assert.IsTrue (act.CanDo(actor.Object, scene.Object));
		}
	}
}

