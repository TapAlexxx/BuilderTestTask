using System;
using BuilderGame.Gameplay.CellControl.Plant;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction.Plant
{
    public class UnitPlower : Interactable
    {
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;

        private PlantCell plantCellToInteract;

        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
        }

        public void StartPlow(PlantCell plantCell)
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