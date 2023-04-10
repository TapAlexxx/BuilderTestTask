using System;
using BuilderGame.Gameplay.Player;
using BuilderGame.Gameplay.Unit.Animation;
using BuilderGame.Gameplay.Unit.Movement;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
{
    public class UnitPlanter : Interactable
    {
        [SerializeField] private UnitActionAnimation unitActionAnimation;
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        
        private PlantCell plantCellToInteract;

        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            unitActionAnimation = GetComponentInChildren<UnitActionAnimation>();
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
        }

        public void StartPlant(PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            plantCellToInteract = plantCell;
            unitActionAnimation.Animate(AnimationType.Plant);
        }

        private void Start() => 
            animationEventCallbacks.Planted += Plant;

        private void OnDestroy() => 
            animationEventCallbacks.Planted -= Plant;

        private void Plant()
        {
            plantCellToInteract.Plant();
            unitActionAnimation.Disable();
            EndedInteract?.Invoke();
        }
    }
}