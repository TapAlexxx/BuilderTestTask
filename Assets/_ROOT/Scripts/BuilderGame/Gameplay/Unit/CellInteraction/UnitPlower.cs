using System;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction
{
    public class UnitPlower : Interactable
    {
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;

        private CellControl.PlantCells.PlantCell plantCellToInteract;

        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
        }

        public void StartPlow(CellControl.PlantCells.PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            plantCellToInteract = plantCell;
        }

        private void Start() => 
            animationEventCallbacks.Plowed += Plow;

        private void OnDestroy() => 
            animationEventCallbacks.Plowed -= Plow;

        private void Plow()
        {
            plantCellToInteract.Plow();
            EndedInteract?.Invoke();
        }
    }
}