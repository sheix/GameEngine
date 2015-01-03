using NUnit.Framework;
using System;
using Game.Acts;
using Engine.Contracts;
using Moq;

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

			IAct act = new TakeAct ();

			Assert.IsTrue (act.CanDo(actor.Object, scene.Object));
		}
	}
}

