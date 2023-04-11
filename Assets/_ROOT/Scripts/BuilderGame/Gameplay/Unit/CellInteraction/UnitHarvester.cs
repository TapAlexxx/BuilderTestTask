using System;
using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Gameplay.Plants;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction
{
    public class UnitHarvester : MonoBehaviour
    {
        [SerializeField] private HarvestPlantAnimation harvestPlantAnimation;
        
        public event Action Harvested;

        private void OnValidate()
        {
            harvestPlantAnimation = GetComponentInChildren<HarvestPlantAnimation>();
        }

        public void Harvest(PlantCell plantCell)
        {
            plantCell.Harvest();
            Plant plant = plantCell.GetPlant();
            harvestPlantAnimation.Animate(plant, OnAnimated);
        }

        private void OnAnimated()
        {
            Harvested?.Invoke();
        }
    }
}