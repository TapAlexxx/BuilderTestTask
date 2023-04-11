using System;
using BuilderGame.Gameplay.CellControl.PlantCells;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction
{
    public abstract class CellInteractable : MonoBehaviour 
    {
        public abstract event Action StartedInteract;
        public abstract event Action EndedInteract;

        public abstract void StartInteract(PlantCell plantCell);
        public abstract void Interact();
    }
}