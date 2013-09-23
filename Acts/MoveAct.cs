using System;
using CursesTest.OfficeRatScene;
using Engine;
using Engine.Interfaces;

namespace CursesTest.Acts
{
    public class MoveAct : IDirectionalAct, IKeyedAct
    {
        private readonly Vector _direction;
        private readonly ConsoleKeyInfo _key;

        public MoveAct(Vector direction, ConsoleKeyInfo key)
        {
            _direction = direction;
            _key = key;
        }

        public bool CanDo(IScene scene)
        {
            var coords = (scene as IOfficeRatScene).Grid1.GetActorCoordinates(Self);
			var cell = (scene as IOfficeRatScene).Grid1.At(coords._x + _direction._x,coords._y + _direction._y);
			if (cell.Actor != null) return false;
			return true;
        }

        public void Do(IScene scene)
        {
			var coords = (scene as IOfficeRatScene).Grid1.GetActorCoordinates(Self);
			var newcoords = coords + _direction;
			(scene as IOfficeRatScene).PlaceActorToGrid((IPlacableActor)Self, newcoords);
			scene.RemoveActor(Self);
			//scene.AddActor(Self);
        }

        public IActor Self
        {
            get; set;
        }

        public string Name
        {
            get { return "Move(" + _direction._x + ", " + _direction._y + ")"; }
            set {  }
        }

        public Vector Direction
        {
            get { return _direction; }
        }

        public ConsoleKeyInfo Key
        {
            get { return _key; }
        }
    }
}
