﻿using System;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitActionAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private readonly int plowParameter = Animator.StringToHash("Plow");
        private readonly int plantParameter = Animator.StringToHash("Plant");
        
        private int currentParameter;
        private Vector3 initialScale;

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void Disable()
        {
            animator.SetBool(currentParameter, false);
        }

        public void AnimateHarvest()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(initialScale * 1.3f, 0.15f));
            sequence.Append(transform.DOScale(initialScale, 0.2f));
        }

        public void Animate(AnimationType animationType)
        {
            currentParameter = GetParameter(animationType);
            animator.SetBool(currentParameter, true);
        }

        private void Start() => 
            initialScale = transform.localScale;

        private int GetParameter(AnimationType animationType) =>
            animationType switch
            {
                AnimationType.Plow => plowParameter,
                AnimationType.Plant => plantParameter,
                _ => throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null)
            };
    }
}