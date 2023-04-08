using System;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitActionAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private readonly int plowParameter = Animator.StringToHash("Plow");
        private readonly int plantParameter = Animator.StringToHash("Plant");
        private int currentParameter;

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void Animate(AnimationType animationType)
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

        public void Disable()
        {
            animator.SetBool(currentParameter, false);
        }
    }
}