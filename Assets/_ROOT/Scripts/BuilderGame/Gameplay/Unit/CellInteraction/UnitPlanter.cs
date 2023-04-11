using System;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction
{
    public class UnitPlanter : Interactable
    {
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        
        private CellControl.PlantCells.PlantCell plantCellToInteract;

        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
        }

        public void StartPlant(CellControl.PlantCells.PlantCell plantCell)
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