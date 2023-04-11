using System;
using BuilderGame.Gameplay.CellControl.Plant;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction.Plant
{
    public class UnitPlanter : Interactable
    {
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        
        private PlantCell plantCellToInteract;

        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
        }

        public void StartPlant(PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            plantCellToInteract = plantCell;
        }

        private void Start() => 
            animationEventCallbacks.Planted += Plant;

        private void OnDestroy() => 
            animationEventCallbacks.Planted -= Plant;

        private void Plant()
        {
            plantCellToInteract.Plant();
            EndedInteract?.Invoke();
        }
    }
}