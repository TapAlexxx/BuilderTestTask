using System;
using BuilderGame.Gameplay.Unit.CellInteraction;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitActionAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private UnitPlower unitPlower;
        [SerializeField] private UnitPlanter unitPlanter;
        
        private readonly int plowParameter = Animator.StringToHash("Plow");
        private readonly int plantParameter = Animator.StringToHash("Plant");
        
        private int currentParameter;
        private Vector3 initialScale;

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
            unitPlower = GetComponentInChildren<UnitPlower>();
            unitPlanter = GetComponentInChildren<UnitPlanter>();
        }

        private void Start()
        {
            initialScale = transform.localScale;

            unitPlower.StartedInteract += AnimatePlowing;
            unitPlanter.StartedInteract += AnimatePlanting;
            
            unitPlower.EndedInteract += Disable;
            unitPlanter.EndedInteract += Disable;
        }

        private void OnDestroy()
        {
            unitPlower.StartedInteract -= AnimatePlowing;
            unitPlanter.StartedInteract -= AnimatePlanting;

            unitPlower.EndedInteract -= Disable;
            unitPlanter.EndedInteract -= Disable;
        }

        private void AnimatePlanting() => 
            Animate(AnimationType.Plant);

        private void AnimatePlowing() => 
            Animate(AnimationType.Plow);

        private void Animate(AnimationType animationType)
        {
            currentParameter = GetParameter(animationType);
            animator.SetBool(currentParameter, true);
        }

        private int GetParameter(AnimationType animationType) =>
            animationType switch
            {
                AnimationType.Plow => plowParameter,
                AnimationType.Plant => plantParameter,
                _ => throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null)
            };

        private void Disable()
        {
            animator.SetBool(currentParameter, false);
        }
    }
}