using BuilderGame.Gameplay.Unit.CellInteraction;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class HarvestParticleAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem harvestParticle;
        [SerializeField] private UnitHarvester unitHarvester;

        private void OnValidate()
        {
            unitHarvester = GetComponentInChildren<UnitHarvester>();
        }

        private void Start() =>
            unitHarvester.Harvested += AnimateHarvest;

        private void OnDestroy() => 
            unitHarvester.Harvested -= AnimateHarvest;


        private void AnimateHarvest() => 
            harvestParticle.Play();
    }
}