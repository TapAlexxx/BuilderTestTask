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

        private void OnValidate()
        {
            unitHarvester = GetComponentInChildren<UnitHarvester>();
            unitCellObserver = GetComponentInChildren<UnitCellObserver>();
        }

        private void Update()
        {
            if(unitCellObserver.TryGetInteractable(out PlantCell plantCell)) 
                TryInteractWithCell(plantCell);
        }

        private void TryInteractWithCell(PlantCell plantCell)
        {
            if(plantCell == null || !plantCell.Interactable)
                return;
            
            switch(plantCell.CurrentState)
            {
                case PlantCellState.Grown:
                    unitHarvester.Harvest(plantCell);
                    break;
            }
        }
    }
}