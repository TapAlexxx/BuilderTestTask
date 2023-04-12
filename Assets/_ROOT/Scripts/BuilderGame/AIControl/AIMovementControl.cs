using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Gameplay.Unit.Movement;
using UnityEngine;

namespace BuilderGame.AIControl
{
    public class AIMovementControl : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        [SerializeField] private Transform waitPosition;
        
        private Transform currentTarget;
        private PlantGrid grid;
        private bool initialized;

        private void OnValidate()
        {
            unitMovement = GetComponentInChildren<UnitMovement>();
        }

        public void Initialize(PlantGrid plantGrid)
        {
            grid = plantGrid;
            initialized = true;
        }

        private void Update()
        {
            if(!initialized)
                return;
            
            if(currentTarget == null && grid.TryGetHarvestable(out PlantCell plantCell))
                currentTarget = plantCell.transform;
            else
            {
                if(currentTarget == null)
                    currentTarget = waitPosition;
            }

            if(currentTarget != null)
                Move();
        }

        private void Stop()
        {
            unitMovement.SetMovementDirection(Vector3.zero);
            currentTarget = null;
        }

        private void Move()
        {
            if (Vector3.Distance(currentTarget.position, transform.position) <= 0.1f)
            {
                Stop();
                return;
            }

            Vector3 direction = GetMovementDirection();
            unitMovement.SetMovementDirection(direction);
        }

        private Vector3 GetMovementDirection()
        {
            var movementVector = currentTarget.position - transform.position;
            movementVector.y = 0f;
            movementVector.Normalize();
            return movementVector;
        }
    }
}