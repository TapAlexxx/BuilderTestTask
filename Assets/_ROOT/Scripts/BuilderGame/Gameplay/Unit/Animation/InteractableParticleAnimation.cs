using BuilderGame.Gameplay.Unit.CellInteraction;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class InteractableParticleAnimation : MonoBehaviour
    {
        [SerializeField] private CellInteractable cellInteractor;
        [SerializeField] private ParticleSystem interactionParticle;

        private void Start()
        {
            cellInteractor.StartedInteract += StartAnimate;
            cellInteractor.EndedInteract += StopAnimate;
        }

        private void OnDestroy()
        {
            cellInteractor.StartedInteract -= StartAnimate;
            cellInteractor.EndedInteract -= StopAnimate;
        }

        private void StartAnimate() => 
            SetEmissionState(interactionParticle, true);

        private void StopAnimate() => 
            SetEmissionState(interactionParticle, false);

        private void SetEmissionState(ParticleSystem particle, bool state)
        {
            var emission = particle.emission;
            emission.enabled = state;
        }
    }
}