using System;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
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

        public void Harvest(PlantCell cell)
        {
            StartedInteract?.Invoke();
            unitActionAnimation.AnimateHarvest();
            cell.Harvest();
            EndedInteract?.Invoke();
        }

    }
}