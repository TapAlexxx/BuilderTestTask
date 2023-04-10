using System.Collections.Generic;
using System.Xml.Linq;
using BuilderGame.Gameplay.Player;
using BuilderGame.Gameplay.Player.Movement;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        [SerializeField] private UnitActionAnimation unitActionAnimation;
        [SerializeField] private PlayerMovementControl playerMovement;
        [SerializeField] private ItemChanger itemChanger;
        
        private PlantCell plantCellToInteract;
        private List<PlantCell> availableCells;
        private bool interacting;

        private void OnValidate()
        {
            triggerObserver = GetComponentInChildren<TriggerObserver>();
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
            unitActionAnimation = GetComponentInChildren<UnitActionAnimation>();
            playerMovement = GetComponentInChildren<PlayerMovementControl>();
            itemChanger = GetComponentInChildren<ItemChanger>();
        }

        private void Start()
        {
            playerMovement.Activate();
            availableCells = new List<PlantCell>();
            triggerObserver.TriggerEnter += OnTriggerEntered;
            triggerObserver.TriggerExit += OnTriggerExited;
            animationEventCallbacks.Plowed += Plow;
            animationEventCallbacks.Planted += Plant;
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= OnTriggerEntered;
            triggerObserver.TriggerExit -= OnTriggerExited;
            animationEventCallbacks.Plowed -= Plow;
            animationEventCallbacks.Planted -= Plant;
            
            foreach (PlantCell plantCell in availableCells) 
                plantCell.BecameInteractable -= AddInAvailable;
        }

        private void OnTriggerEntered(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell)) 
                AddInAvailable(cell);
        }

        private void OnTriggerExited(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell))
            {
                availableCells.Remove(cell);
                cell.BecameInteractable -= AddInAvailable;
            }
        }

        private void Update()
        {
            if(interacting)
                return;
           
            if(availableCells.Count > 0) 
                TryInteractWithCell(availableCells[0]);
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
            interacting = true;
            plantCellToInteract = plantCell;
            switch(plantCell.CurrentState)
            {
                case PlantCellState.Grass:
                    StartPlow();
                    break;
                case PlantCellState.Plowed:
                    StartPlant();
                    break;
                case PlantCellState.Grown:
                    Harvest();
                    break;
                case PlantCellState.Harvested:
                    break;
            }
        }

        private void StartPlow()
        {
            playerMovement.Disable();
            itemChanger.Take(ItemType.Plowing);
            unitActionAnimation.Animate(AnimationType.Plow);
        }

        private void StartPlant()
        {
            playerMovement.Disable();
            itemChanger.Take(ItemType.Planting);
            unitActionAnimation.Animate(AnimationType.Plant);
        }

        private void Plow()
        {
            plantCellToInteract.Plow();
            ActivateMovement();
        }

        private void Plant()
        {
            plantCellToInteract.Plant();
            ActivateMovement();
        }

        private void Harvest()
        {
            unitActionAnimation.AnimateHarvest();
            plantCellToInteract.Harvest();
            interacting = false;
        }

        private void ActivateMovement()
        {
            unitActionAnimation.Disable();
            itemChanger.HideIfExist();
            playerMovement.Activate();
            interacting = false;
        }
    }
}