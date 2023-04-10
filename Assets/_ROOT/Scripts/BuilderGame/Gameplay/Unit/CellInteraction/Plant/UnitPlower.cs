using System;
using BuilderGame.Gameplay.Player;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
{
    public class UnitPlower : Interactable
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

        private void Start() => 
            animationEventCallbacks.Plowed += Plow;

        private void OnDestroy() => 
            animationEventCallbacks.Plowed -= Plow;

        public void StartPlow(PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            plantCellToInteract = plantCell;
            unitActionAnimation.Animate(AnimationType.Plow);
        }

        private void Plow()
        {
            plantCellToInteract.Plow();
            unitActionAnimation.Disable();
            EndedInteract?.Invoke();
        }
    }
}