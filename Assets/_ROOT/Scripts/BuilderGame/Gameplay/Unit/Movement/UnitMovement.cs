using System;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class UnitMovement : MonoBehaviour
    {
        [Header("Settings")]
        public float Speed;
        
        [SerializeField] 
        private float smoothness = 0.1f;
        
        [Header("References")]
        [SerializeField]
        private CharacterController characterController;

        public Vector3 Direction { get; private set; } = Vector3.zero;
        public Vector3 TargetVelocity { get; private set; }
        
        private Vector3 smoothedVelocity;
        private Vector3 currentVelocity;
        private bool active;

        private void OnValidate()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Awake()
        {
            Activate();
        }

        public void SetMovementDirection(Vector3 direction)
        {
            if(!active)
                return;
            Direction = direction;
        }
        
        public void Disable()
        {
            active = false;
            SetMovementDirection(Vector3.zero);
            characterController.Move(Vector3.zero);
        }

        public void Activate()
        {
            active = true;
        }
        
        private void Update()
        {
            if(!active)
                return;
            Move();
        }

        private void Move()
        {
            TargetVelocity = Vector3.zero;

            if (Direction.sqrMagnitude > Constants.Epsilon)
            {
                TargetVelocity = Direction * Speed;
            }
            
            smoothedVelocity =
                    Vector3.SmoothDamp(smoothedVelocity, TargetVelocity + Physics.gravity, 
                        ref currentVelocity, smoothness);
            
            characterController.Move(smoothedVelocity * Time.deltaTime);
        }
    }
}