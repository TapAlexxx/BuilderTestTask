using UnityEngine;

namespace BuilderGame.AIControl
{
    public class RewardedAdAnimation : MonoBehaviour
    {
        [SerializeField] private RewardedAIActivator rewardedAIActivator;
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private Renderer renderer;
        
        private void OnValidate()
        {
            rewardedAIActivator = GetComponentInChildren<RewardedAIActivator>();
            renderer = GetComponentInChildren<Renderer>();
        }

        private void Start()
        {
            rewardedAIActivator.Watched += Animate;
        }

        private void OnDestroy()
        {
            rewardedAIActivator.Watched -= Animate;
        }

        private void Animate()
        {
            particleSystem.Play();
            renderer.enabled = false;
        }
    }
}