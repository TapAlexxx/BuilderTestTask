using BuilderGame.Gameplay.Unit.Rotation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction
{
    public class UnitTargetLocker : MonoBehaviour
    {
        [SerializeField] private UnitRotation unitRotation;
        [SerializeField] private MovementControlRotation movementControlRotation;
        
        private void OnValidate()
        {
            unitRotation = GetComponentInChildren<UnitRotation>();
            movementControlRotation = GetComponentInChildren<MovementControlRotation>();
        }

        public void LockTo(Transform target)
        {
            movementControlRotation.Disable();
            LookAt(target);
        }

        public void Unlock()
        {
            movementControlRotation.Activate();
        }

        private void LookAt(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            unitRotation.SetRotationDirection(direction);
        }
    }
}