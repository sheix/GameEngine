using System;
using System.Linq;
using Engine.Contracts;
using Game;
using Game.Cells;

namespace LevelGeneratorTester
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			for (int i=0; i<100; i++) {
				ISceneFactory sceneGenerator = new SceneFactory (new DefaultActorFactory());
				IScene scene = sceneGenerator.GetScene ("Default", "None");
				IRenderable renderable = new ASCIIFileRenerer ();
				renderable.RenderScene (scene);
			}
		}
	}

    internal class DefaultActorFactory : IActorFactory
    {
        public IPlacableActor GetPlayer()
        {
            return new Player(null);
        }

        public IPlacableActor GetActor()
        {
            throw new NotImplementedException();
        }
    }

    internal class ASCIIFileRenerer : IRenderable
    {
        public void RenderMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void RenderCalendar(ICalendar calendar)
        {
            throw new NotImplementedException();
        }

        public void RenderScene(IScene scene)
        {
			var gameScene = scene as GameScene;
            for (int x = 0; x < gameScene.GetMapDimensions()._x; x++)
            {
				for (int y = 0; y < gameScene.GetMapDimensions()._y; y++)
                {
                    ICell cell = gameScene.At(x, y);
                    
                    if (cell.Actor != null)
                        if (cell.Actor.Name == "Player")
                        {
                            Console.Write("@");
                            continue;
                        }
                    if (cell is Wall)
                    {
                        Console.Write("#");
                        continue;
                    }
                    if (cell.Specials != null)
                    {
                        if (cell.Specials.Any(m => m is EndPoint))
                        {
                            
                            Console.Write(">");
                            continue;
                        }
                        if (cell.Specials.Any(m => m is StartPoint))
                        {

                            Console.Write("<");
                            continue;
                        }
                    }
                    if (cell.Actor == null)
                    {
                        Console.Write(".");
                        continue;
                    }
                }
                Console.WriteLine();

            }
            Console.ReadLine();
        }
    }
}
