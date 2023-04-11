using System;
using BuilderGame.Gameplay.CellControl.Plant;

namespace BuilderGame.Gameplay.Unit.CellInteraction.Plant
{
    public class UnitHarvester : Interactable
    {
        public override event Action StartedInteract;
        public override event Action EndedInteract;

        public void Harvest(PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            plantCell.Harvest();
            EndedInteract?.Invoke();
        }

    }
}