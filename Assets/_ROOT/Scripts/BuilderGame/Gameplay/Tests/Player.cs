using BuilderGame.Gameplay.Player;
using BuilderGame.Gameplay.Player.Movement;
using BuilderGame.Gameplay.Unit.Animation;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Tests
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        [SerializeField] private UnitActionAnimation unitActionAnimation;
        [SerializeField] private PlayerMovementControl playerMovement;
        [SerializeField] private ItemChanger itemChanger;
        
        private Cell cellToInteract;

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
            if (obj.TryGetComponent(out Cell cell))
            {
                TryInteractWithCell(cell);
            }
        }

        private void TryInteractWithCell(Cell cell)
        {
            Debug.Log(cell.CurrentState + " " + cell.AbleToSwitchState);
            if(!cell.AbleToSwitchState)
                return;
            cellToInteract = cell;
            switch(cell.CurrentState)
            {
                case PlantState.Grass:
                    StartPlow();
                    break;
                case PlantState.Plowed:
                    StartPlant();
                    break;
                case PlantState.Planted:
                    break;
                case PlantState.Growing:
                    break;
                case PlantState.Harvestable:
                    Harvest(cell);
                    break;
                case PlantState.Harvested:
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
            unitActionAnimation.Disable();
            cellToInteract.Plant();
            itemChanger.HideIfExist();
            playerMovement.Activate();
        }

        private void Plow()
        {
            cellToInteract.Plow();
            unitActionAnimation.Disable();
            itemChanger.HideIfExist();
            if (cellToInteract.AbleToSwitchState)
            {
                TryInteractWithCell(cellToInteract);
            }
            else
            {
                playerMovement.Activate();
            }
        }

        private void Harvest(Cell cell)
        {
            unitActionAnimation.AnimateHarvest();
            cell.Harvest();
        }
    }
}