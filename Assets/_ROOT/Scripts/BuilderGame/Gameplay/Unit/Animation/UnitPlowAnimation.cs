using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitPlowAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private readonly int farmingParameter = Animator.StringToHash("Farming");

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                Animate();
            if (Input.GetKeyDown(KeyCode.F))
                StopAnimation();
        }

        public void Animate()
        {
            animator.SetBool(farmingParameter, true);
        }

        public void StopAnimation()
        {
            animator.SetBool(farmingParameter, false);
        }
    }
}