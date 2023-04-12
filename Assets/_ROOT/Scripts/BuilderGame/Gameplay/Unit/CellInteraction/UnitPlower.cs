﻿using System;
using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Gameplay.Unit.Animation;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.CellInteraction
{
    public class UnitPlower : CellInteractable
    {
        [SerializeField] private AnimationEventCallbacks animationEventCallbacks;
        [SerializeField] private UnitTargetLocker unitTargetLocker;

        private PlantCell plantCellToInteract;

        public override event Action StartedInteract;
        public override event Action EndedInteract;

        private void OnValidate()
        {
            animationEventCallbacks = GetComponentInChildren<AnimationEventCallbacks>();
            unitTargetLocker = GetComponentInChildren<UnitTargetLocker>();
        }

        public override void StartInteract(PlantCell plantCell)
        {
            StartedInteract?.Invoke();
            unitTargetLocker.LockTo(plantCell.transform);
            plantCellToInteract = plantCell;
        }

        public override void Interact()
        {
            plantCellToInteract.Plow();
            unitTargetLocker.Unlock();
            EndedInteract?.Invoke();
        }

        private void Start() => 
            animationEventCallbacks.Plowed += Interact;

        private void OnDestroy() => 
            animationEventCallbacks.Plowed -= Interact;
    }
}