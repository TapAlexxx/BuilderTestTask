using System;
using BuilderGame.Gameplay.Unit.CellInteraction;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitParticleAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem plowParticle;
        [SerializeField] private ParticleSystem plantingParticle;
        [SerializeField] private ParticleSystem harvestParticle;
        [SerializeField] private UnitPlower unitPlower;
        [SerializeField] private UnitPlanter unitPlanter;
        [SerializeField] private UnitHarvester unitHarvester;

        private void OnValidate()
        {
            unitPlower = GetComponentInChildren<UnitPlower>();
            unitPlanter = GetComponentInChildren<UnitPlanter>();
            unitHarvester = GetComponentInChildren<UnitHarvester>();
        }

        private void Start()
        {
            unitPlower.StartedInteract += StartAnimatePlowing;
            unitPlower.EndedInteract += StopAnimatePlowing;
            
            unitPlanter.StartedInteract += StartAnimatePlanting;
            unitPlanter.EndedInteract += StopAnimatePlanting;

            unitHarvester.Harvested += AnimateHarvest;
        }

        private void OnDestroy()
        {
            unitPlower.StartedInteract += StartAnimatePlowing;
            unitPlower.EndedInteract += StopAnimatePlowing;

            unitPlanter.StartedInteract -= StartAnimatePlanting;
            unitPlanter.EndedInteract -= StopAnimatePlanting;
            
            unitHarvester.Harvested -= AnimateHarvest;
        }

        private void StartAnimatePlanting() => 
            SetEmissionState(plantingParticle, true);

        private void StartAnimatePlowing() => 
            SetEmissionState(plowParticle, true);

        private void StopAnimatePlowing() => 
            SetEmissionState(plowParticle, false);

        private void StopAnimatePlanting() => 
            SetEmissionState(plantingParticle, false);

        private void SetEmissionState(ParticleSystem particle, bool state)
        {
            var emission = particle.emission;
            emission.enabled = state;
        }

        private void AnimateHarvest() => 
            harvestParticle.Play();
    }
}