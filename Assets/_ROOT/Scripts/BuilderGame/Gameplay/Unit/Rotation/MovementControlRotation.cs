using System;
using BuilderGame.Gameplay.Unit.Movement;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Rotation
{
    [RequireComponent(typeof(UnitRotation), typeof(UnitMovement))]
    public class MovementControlRotation : MonoBehaviour
    {
        [SerializeField] private UnitRotation unitRotation;
        [SerializeField] private UnitMovement unitMovement;
        
        private bool active;

        private void OnValidate()
        {
            unitRotation = GetComponent<UnitRotation>();
            unitMovement = GetComponent<UnitMovement>();
        }

        private void Awake() => 
            Activate();

        public void Activate() => 
            active = true;

        public void Disable() => 
            active = false;

        private void Update()
        {
            if(!active)
                return;
            if (unitMovement.TargetVelocity.sqrMagnitude > Constants.Epsilon)
            {
                unitRotation.SetRotationDirection(unitMovement.Direction);
            }
        }
    }
}