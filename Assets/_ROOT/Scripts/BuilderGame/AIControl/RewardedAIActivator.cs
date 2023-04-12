using System;
using BuilderGame.Gameplay;
using BuilderGame.Gameplay.CellControl.PlantCells;
using BuilderGame.Infrastructure.Services.Ads;
using UnityEngine;
using Zenject;

namespace BuilderGame.AIControl
{
    public class RewardedAIActivator : MonoBehaviour
    {
        [SerializeField] private AIInitializer aiInitializer;
        [SerializeField] private PlantGrid gridToConnectAI;
        [SerializeField] private TriggerObserver triggerObserver;
        
        private IAdvertiser advertiser;
        private bool initialized;

        public event Action Watched;

        private void OnValidate()
        {
            triggerObserver = GetComponentInChildren<TriggerObserver>();
        }

        [Inject]
        public void Construct(IAdvertiser advertiser)
        {
            this.advertiser = advertiser;
        }

        private void Start() => 
            triggerObserver.TriggerEnter += OnTriggerEntered;

        private void OnDestroy() => 
            triggerObserver.TriggerEnter -= OnTriggerEntered;

        private void OnTriggerEntered(Collider obj)
        {
            if(initialized) 
                return;
            
            if (obj.CompareTag("Player")) 
                ShowRewarded();
        }

        private async void ShowRewarded()
        {
            AdWatchResult result = await advertiser.ShowRewardedAd("Rewarded");
            
            switch (result)
            {
                case AdWatchResult.Watched:
                    aiInitializer.Initialize(gridToConnectAI);
                    initialized = true;
                    Watched?.Invoke();
                    break;
                case AdWatchResult.Closed:
                    Debug.Log("closed");
                    break;
                default:
                    Debug.Log("went Wrong");
                    break;
            }
        }
    }
}