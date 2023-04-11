using System;
using BuilderGame.Gameplay.Unit.CellInteraction.Plant;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitParticleAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem plantingParticle;
        [SerializeField] private UnitPlanter unitPlanter;

        private void OnValidate()
        {
            unitPlanter = GetComponentInChildren<UnitPlanter>();
        }

        private void Start()
        {
            unitPlanter.StartedInteract += StartAnimatePlanting;
            unitPlanter.EndedInteract += StopAnimatePlanting;
        }

        private void OnDestroy()
        {
            unitPlanter.StartedInteract -= StartAnimatePlanting;
            unitPlanter.EndedInteract -= StopAnimatePlanting;
        }

        private void StartAnimatePlanting()
        {
            var emission = plantingParticle.emission;
            emission.enabled = true;
        }

        private void StopAnimatePlanting()
        {
            var emission = plantingParticle.emission;
            emission.enabled = false;
        }
    }
}