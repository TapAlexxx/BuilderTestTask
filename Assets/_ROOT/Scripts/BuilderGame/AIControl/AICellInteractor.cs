using System;
using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Gameplay.Player;
using BuilderGame.Gameplay.Unit.CellInteraction;
using UnityEngine;

namespace BuilderGame.AIControl
{
    public class AICellInteractor : MonoBehaviour
    {
        [SerializeField] private UnitHarvester unitHarvester;
        [SerializeField] private UnitCellObserver unitCellObserver;
        private PlantCell currentPlantCell;

        private void OnValidate()
        {
            unitHarvester = GetComponentInChildren<UnitHarvester>();
            unitCellObserver = GetComponentInChildren<UnitCellObserver>();
        }

        private void Awake() => 
            currentPlantCell = null;

        private void Start() => 
            unitHarvester.Harvested += Reset;

        private void OnDestroy() => 
            unitHarvester.Harvested -= Reset;

        private void Update()
        {
            if(currentPlantCell != null)
                return;
            
            if(unitCellObserver.TryGetInteractable(out PlantCell plantCell)) 
                TryInteractWithCell(plantCell);
        }

        private void TryInteractWithCell(PlantCell plantCell)
        {
            if(plantCell == null || !plantCell.Interactable)
                return;
            currentPlantCell = plantCell;
            switch(plantCell.CurrentState)
            {
                case PlantCellState.Grown:
                    unitHarvester.Harvest(plantCell);
                    break;
            }
        }

        private void Reset() => 
            currentPlantCell = null;
    }
}