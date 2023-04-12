using System;
using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Gameplay.Unit.Movement;
using UnityEngine;

namespace BuilderGame.AIControl
{
    public class AIMovementControl : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        [SerializeField] private Transform waitPosition;
        [SerializeField] private PlantGrid grid;

        private Transform currentTarget;
        private bool isMoving;

        private void OnValidate()
        {
            unitMovement = GetComponentInChildren<UnitMovement>();
        }


        public void Initialize(PlantGrid plantGrid)
        {
            grid = plantGrid;
        }

        private void Update()
        {
            if(currentTarget == null)
                currentTarget = grid.TryGetHarvestable(out PlantCell plantCell) 
                    ? plantCell.transform 
                    : waitPosition;
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
                isMoving = false;
                return;
            }
            isMoving = true;
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