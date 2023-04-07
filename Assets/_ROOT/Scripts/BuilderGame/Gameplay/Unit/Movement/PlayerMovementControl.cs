﻿using _ROOT.Scripts.BuilderGame.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace _ROOT.Scripts.BuilderGame.Gameplay.Unit.Movement
{
    [RequireComponent(typeof(UnitMovement))]
    public class PlayerMovementControl : MonoBehaviour
    {
        [SerializeField] 
        private UnitMovement unitMovement;
        
        private Camera mainCamera;
        private IInputProvider inputProvider;

        private void OnValidate()
        {
            unitMovement = GetComponent<UnitMovement>();
        }

        [Inject]
        public void Construct(IInputProvider inputProvider)
        {
            this.inputProvider = inputProvider;
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            var input = inputProvider.Axis;

            var movementVector = mainCamera.transform.TransformDirection(input);
            movementVector.y = 0f;
            movementVector.Normalize();
            
            unitMovement.SetMovementDirection(movementVector);
        }
    }
}