using System;
using System.Collections.Generic;
using Engine;
using Engine.Contracts;
using System.Linq;

namespace Game
{
	public class GameScene : Scene
	{
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
	}
}

