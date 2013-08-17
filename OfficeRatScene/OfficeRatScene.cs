﻿using System;
using CursesTest.Factories;
using Engine;
using Engine.Interfaces;
using OfficeRatTest;

namespace CursesTest.OfficeRatScene
{
    public class OfficeRatScene : Scene
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

        override public void AddActor(IActor actor)
        {
            base.AddActor(actor);
            if (actor is IPlacableActor)
                PlaceActorToGrid((IPlacableActor)actor);
        }

        private void PlaceActorToGrid(IPlacableActor actor)
        {
            Grid1.Grid[actor.InitialX][actor.InitialY].Actor = actor;
        }

        public override void Render()
        {
            base.Render();
        }

        
    }
}