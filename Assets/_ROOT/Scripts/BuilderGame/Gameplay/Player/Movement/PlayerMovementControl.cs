using System;
using BuilderGame.Gameplay.Unit.Movement;
using BuilderGame.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.Player.Movement
{
    [RequireComponent(typeof(UnitMovement))]
    public class PlayerMovementControl : MonoBehaviour
    {
        [SerializeField] 
        private UnitMovement unitMovement;
        
        private Camera mainCamera;
        private IInputProvider inputProvider;
        private bool active;

        private void OnValidate()
        {
            unitMovement = GetComponent<UnitMovement>();
        }

        [Inject]
        public void Construct(IInputProvider inputProvider) => 
            this.inputProvider = inputProvider;

        public void Activate() => 
            active = true;

        public void Disable()
        {
            active = false;
            unitMovement.SetMovementDirection(Vector3.zero);
        }

        private void Awake() => 
            Activate();

        private void Start() => 
            mainCamera = Camera.main;

        private void Update()
        {
            if(!active)
                return;
            
            var input = inputProvider.Axis;

            var movementVector = GetMovementVector(input);

            unitMovement.SetMovementDirection(movementVector);
        }

        private Vector3 GetMovementVector(Vector2 input)
        {
            var movementVector = mainCamera.transform.TransformDirection(input);
            movementVector.y = 0f;
            movementVector.Normalize();
            return movementVector;
        }
    }
}