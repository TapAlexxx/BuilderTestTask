using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Gameplay.Unit.CellInteraction;
using UnityEngine;

namespace BuilderGame.Gameplay.Player
{
    public class PlayerCellInteractor : MonoBehaviour
    {
        [SerializeField] private UnitPlower unitPlower;
        [SerializeField] private UnitPlanter unitPlanter;
        [SerializeField] private UnitHarvester unitHarvester;
        [SerializeField] private UnitCellObserver unitCellObserver;
        [SerializeField] private PlayerStateControl playerStateControl;

        private void OnValidate()
        {
            unitPlower = GetComponentInChildren<UnitPlower>();
            unitPlanter = GetComponentInChildren<UnitPlanter>();
            unitHarvester = GetComponentInChildren<UnitHarvester>();
            unitCellObserver = GetComponentInChildren<UnitCellObserver>();
            playerStateControl = GetComponentInChildren<PlayerStateControl>();
        }

        private void Update()
        {
            if(playerStateControl.Interacting)
                return;
           
            if(unitCellObserver.TryGetInteractable(out PlantCell plantCell)) 
                TryInteractWithCell(plantCell);
        }

        private void TryInteractWithCell(PlantCell plantCell)
        {
            if(plantCell == null || !plantCell.Interactable)
                return;
            
            switch(plantCell.CurrentState)
            {
                case PlantCellState.Grass:
                    unitPlower.StartInteract(plantCell);
                    break;
                case PlantCellState.Plowed:
                    unitPlanter.StartInteract(plantCell);
                    break;
                case PlantCellState.Grown:
                    unitHarvester.Harvest(plantCell);
                    break;
            }
        }
    }
}