using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
{
    public class UnitPlantCellInteractor : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private UnitPlower unitPlower;
        [SerializeField] private UnitPlanter unitPlanter;
        [SerializeField] private UnitHarvester unitHarvester;
        [SerializeField] private PlayerStateControl playerStateControl;
        
        private List<PlantCell> availableCells;

        private void OnValidate()
        {
            triggerObserver = GetComponentInChildren<TriggerObserver>();
            unitPlower = GetComponentInChildren<UnitPlower>();
            unitPlanter = GetComponentInChildren<UnitPlanter>();
            unitHarvester = GetComponentInChildren<UnitHarvester>();
            playerStateControl = GetComponentInChildren<PlayerStateControl>();
        }

        private void Start()
        {
            Initialize();
            SubscribeOnTriggerEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromTriggerEvents();
            UnsubscribeFromAvailableCells();
        }

        private void Update()
        {
            if(playerStateControl.Interacting)
                return;
           
            if(availableCells.Count > 0) 
                TryInteractWithCell(availableCells[0]);
        }

        private void Initialize() => 
            availableCells = new List<PlantCell>();

        private void TryAddPlantCell(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell)) 
                AddInAvailable(cell);
        }

        private void TryRemovePlantCell(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell))
            {
                availableCells.Remove(cell);
                cell.BecameInteractable -= AddInAvailable;
            }
        }

        private void AddInAvailable(PlantCell cell)
        {
            if(availableCells.Contains(cell))
                return;
            availableCells.Add(cell);
            cell.BecameInteractable += AddInAvailable;
        }

        private void TryInteractWithCell(PlantCell plantCell)
        {
            if(!plantCell.Interactable)
                return;
            switch(plantCell.CurrentState)
            {
                case PlantCellState.Grass:
                    unitPlower.StartPlow(plantCell);
                    break;
                case PlantCellState.Plowed:
                    unitPlanter.StartPlant(plantCell);
                    break;
                case PlantCellState.Grown:
                    unitHarvester.Harvest(plantCell);
                    break;
            }
        }

        private void SubscribeOnTriggerEvents()
        {
            triggerObserver.TriggerEnter += TryAddPlantCell;
            triggerObserver.TriggerExit += TryRemovePlantCell;
        }

        private void UnsubscribeFromTriggerEvents()
        {
            triggerObserver.TriggerEnter -= TryAddPlantCell;
            triggerObserver.TriggerExit -= TryRemovePlantCell;
        }

        private void UnsubscribeFromAvailableCells()
        {
            foreach (PlantCell plantCell in availableCells)
                plantCell.BecameInteractable -= AddInAvailable;
        }
    }
}