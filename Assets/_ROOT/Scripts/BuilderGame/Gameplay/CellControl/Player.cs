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

            triggerObserver.TriggerEnter += ValidateTrigger;
            animationEventCallbacks.Plowed += Plow;
            animationEventCallbacks.Planted += Plant;
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= ValidateTrigger;
            animationEventCallbacks.Plowed -= Plow;
            animationEventCallbacks.Planted -= Plant;
        }

        private void ValidateTrigger(Collider obj)
        {
            if (obj.TryGetComponent(out PlantCell cell))
            {
                TryInteractWithCell(cell);
            }
        }

        private void TryInteractWithCell(PlantCell plantCell)
        {
            if(!plantCell.Interactable)
                return;
            plantCellToInteract = plantCell;
            switch(plantCell.CurrentState)
            {
                case CellState.Grass:
                    StartPlow();
                    break;
                case CellState.Plowed:
                    StartPlant();
                    break;
                case CellState.Grown:
                    Harvest();
                    break;
                case CellState.Harvested:
                    break;
            }
        }

        private void StartPlow()
        {
            itemChanger.Take(ItemType.Plowing);
            playerMovement.Disable();
            unitActionAnimation.Animate(AnimationType.Plow);
        }

        private void StartPlant()
        {
            itemChanger.Take(ItemType.Planting);
            playerMovement.Disable();
            unitActionAnimation.Animate(AnimationType.Plant);
        }

        private void Plant()
        {
            plantCellToInteract.Plant();
            unitActionAnimation.Disable();
            itemChanger.HideIfExist();
            playerMovement.Activate();
        }

        private void Plow()
        {
            plantCellToInteract.Plow();
            unitActionAnimation.Disable();
            itemChanger.HideIfExist();
            
            if (plantCellToInteract.Interactable)
                TryInteractWithCell(plantCellToInteract);
            else
                playerMovement.Activate();
        }

        private void Harvest()
        {
            unitActionAnimation.AnimateHarvest();
            plantCellToInteract.Harvest();
        }
    }
}