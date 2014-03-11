using System;

namespace Contracts
{
	public interface IStage
    {
        IGrid Grid1 { get; }
        void PlaceActorToGrid(IPlacableActor actor);
        void PlaceActorToGrid(IPlacableActor actor, Vector v);

    }
}

