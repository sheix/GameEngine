using System;
using System.Collections.Generic;
using Engine;
using Engine.Contracts;
using System.Linq;

namespace Game
{
	public class GameScene : BaseScene
	{
		public GameScene(String ID): base(ID)
		{}

		public Dictionary<string, Vector> GetStartingPoints ()
		{
			return Map.GetCells(x => x.Specials.Any(m => m is StartPoint), y => y.Specials.Where(m => m is StartPoint).FirstOrDefault().Description);
		}
		public Dictionary<string, Vector> GetEndingPoints ()
		{
			return Map.GetCells(x => x.Specials.Any(m => m is EndPoint), y => y.Specials.Where(m => m is EndPoint).FirstOrDefault().Description);
		}

		public Vector GetPlayerCoordinates ()
		{
			return Map.GetActorCoordinates ("Player");
		}

		public ICell At (int x, int y)
		{
			return Map.At(x,y);
		}

		public bool HaveItemsBeneath(IActor actor)
		{
			var coordinates = Map.GetActorCoordinates (actor);
			var items = Map.At (coordinates).Items;
			return items == null ? false : items.Count > 0; 
		}
	}
}

