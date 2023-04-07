using UnityEngine;

namespace _ROOT.Scripts.BuilderGame.Infrastructure.Ads.Fake
{
    [CreateAssetMenu(fileName = nameof(FakeAdsSettings), menuName = "Ads/Fake/Settings", order = 0)]
    public class FakeAdsSettings : ScriptableObject
    {
        public FakeAdsWindow FakeAdsWindowPrefab;
    }
}