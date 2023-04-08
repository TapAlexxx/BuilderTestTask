using BuilderGame.Gameplay.Player.Movement;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Tests
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        [SerializeField] private UnitActionAnimation unitActionAnimation;
        [SerializeField] private PlayerMovementControl playerMovement;
        
        private Cell cellToInteract;

        private void OnValidate()
        {
            triggerObserver = GetComponentInChildren<TriggerObserver>();
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
            unitActionAnimation = GetComponentInChildren<UnitActionAnimation>();
            playerMovement = GetComponentInChildren<PlayerMovementControl>();
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
                    cell.Harvest();
                    break;
                case PlantState.Harvested:
                    break;
            }
        }

        private void StartPlow()
        {
            playerMovement.Disable();
            unitActionAnimation.Animate(AnimationType.Plow);
        }

        private void StartPlant()
        {
            playerMovement.Disable();
            unitActionAnimation.Animate(AnimationType.Plant);
        }

        private void Plant()
        {
            unitActionAnimation.Disable();
            cellToInteract.Plant();
            playerMovement.Activate();
        }

        private void Plow()
        {
            cellToInteract.Plow();
            unitActionAnimation.Disable();
            if (cellToInteract.AbleToSwitchState)
            {
                TryInteractWithCell(cellToInteract);
            }
            else
            {
                playerMovement.Activate();
            }
        }
    }
}