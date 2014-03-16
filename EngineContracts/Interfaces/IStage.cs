using System;

namespace Contracts
{
	public interface IStage
    {
        IGrid Map { get; }
        void PlaceActorToGrid(IPlacableActor actor);
        void PlaceActorToGrid(IPlacableActor actor, Vector v);

    }
}

