using OfficeRat.Factories;
using Engine;
using Engine.Interfaces;

namespace OfficeRat
{
    public class Stage : Scene, IStage
    {
        IGrid _grid;
        public Stage()
        {
            ILevelFactory factory = new LevelFactory();
            _grid = factory.GenerateGrid();
        }

        public IGrid Grid1
        {
            get { return _grid; }
        }

        public override void AddActor(IActor actor)
        {
            if (actor is IPlacableActor)
            {
                PlaceActorToGrid(actor as IPlacableActor);
            }
            base.AddActor(actor);
        }

        public void PlaceActorToGrid(IPlacableActor actor)
        {
            Grid1.Grid[actor.InitialX][actor.InitialY].Actor = actor;
        }

        public void PlaceActorToGrid(IPlacableActor actor, Vector v)
        {
            Grid1.Grid[v._x][v._y].Actor = actor;
        }

        override public void RemoveActor(IActor actor)
        {
            if (actor is IPlacableActor)
            {
                var coords = _grid.GetActorCoordinates(actor);
                _grid.At(coords).Actor = null;
            }
            base.RemoveActor(actor);
        }

        public override void Render()
        {
            Grid1.Render();
            base.Render();
        }


    }

    public interface IStage : IScene
    {
        IGrid Grid1 { get; }
        void PlaceActorToGrid(IPlacableActor actor);
        void PlaceActorToGrid(IPlacableActor actor, Vector v);

    }
}
