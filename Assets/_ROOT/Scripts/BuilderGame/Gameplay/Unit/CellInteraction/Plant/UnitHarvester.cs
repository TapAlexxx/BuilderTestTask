using System;
using BuilderGame.Gameplay.CellControl.Plant;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction.Plant
{
    public class UnitHarvester : Interactable
    {
        [SerializeField] private UnitActionAnimation unitActionAnimation;
        
        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            unitActionAnimation = GetComponentInChildren<UnitActionAnimation>();
        }

        public void Harvest(PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            unitActionAnimation.AnimateHarvest();
            plantCell.Harvest();
            EndedInteract?.Invoke();
        }

    }
}