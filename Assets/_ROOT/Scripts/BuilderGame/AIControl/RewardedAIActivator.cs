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

        private void OnValidate()
        {
            triggerObserver = GetComponentInChildren<TriggerObserver>();
        }

        [Inject]
        public void Construct(IAdvertiser advertiser)
        {
            this.advertiser = advertiser;
        }

        private void Start()
        {
            triggerObserver.TriggerEnter += OnTriggerEntered;
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider obj)
        {
            if (obj.CompareTag("Player"))
            {
                ShowRewarded();
            }
        }

        private async void ShowRewarded()
        {
            AdWatchResult result = await advertiser.ShowRewardedAd("Rewarded");
            
            switch (result)
            {
                case AdWatchResult.Watched:
                    Debug.Log("watched");
                    break;
                case AdWatchResult.Closed:
                    Debug.Log("closed");
                    break;
                default:
                    Debug.Log("wentWrong");
                    break;
            }
        }
    }
}