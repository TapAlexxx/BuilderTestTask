using System.Collections.Generic;
using System.Linq;
using BuilderGame.Gameplay.CellControl.PlantCells;
using UnityEngine;

namespace BuilderGame.Gameplay.Player
{
    public class UnitCellObserver : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;

        private List<PlantCell> availableCells;

        private void OnValidate()
        {
            triggerObserver = GetComponentInChildren<TriggerObserver>();
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

        private void Initialize() => 
            availableCells = new List<PlantCell>();
        
        private void AddInAvailable(PlantCell cell)
        {
            if(availableCells.Contains(cell))
                return;
            availableCells.Add(cell);
            cell.BecameInteractable += AddInAvailable;
        }

        private void RemoveFromAvailable(PlantCell cell)
        {
            availableCells.Remove(cell);
            cell.BecameInteractable -= AddInAvailable;
        }

        private void TryAddPlantCell(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell))
            {
                AddInAvailable(cell);
            }
        }

        private void TryRemovePlantCell(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell)) 
                RemoveFromAvailable(cell);
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

        public bool TryGetInteractable(out PlantCell plantCell)
        {
            plantCell = availableCells.FirstOrDefault(x => x.Interactable);
            return plantCell != null;
        }
    }
}