﻿using System;
using CursesTest.Factories;
using Engine;
using Engine.Interfaces;
using OfficeRatTest;

namespace CursesTest.OfficeRatScene
{
    public class OfficeRatScene : Scene, IOfficeRatScene
    {
        IGrid _grid;
        public OfficeRatScene()
        {
            ILevelFactory factory = new LevelFactory();
            _grid = factory.GenerateGrid();
        }

        public IGrid Grid1
        {
            get { return _grid; }
        }

		public override void AddActor (IActor actor)
		{
			if (actor is IPlacableActor)
			{
				PlaceActorToGrid(actor as IPlacableActor);
			}
			base.AddActor (actor);
		}

        public void PlaceActorToGrid(IPlacableActor actor)
        {
            Grid1.Grid[actor.InitialX][actor.InitialY].Actor = actor;
        }

		public void PlaceActorToGrid(IPlacableActor actor, Vector v)
        {
            Grid1.Grid[v._x][v._y].Actor = actor;
        }

		override public void RemoveActor (IActor actor)
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
            base.Render();
        }

        
    }

    public interface IOfficeRatScene : IScene
    {
        IGrid Grid1 { get; }
        void PlaceActorToGrid(IPlacableActor actor);
		void PlaceActorToGrid(IPlacableActor actor, Vector v);

    }
}
