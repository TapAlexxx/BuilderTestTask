using System.Threading.Tasks;

namespace _ROOT.Scripts.BuilderGame.Infrastructure.Ads
{
    public interface IAdvertiser
    {
        Task<AdWatchResult> ShowInterstitialAd(string placement);
        Task<AdWatchResult> ShowRewardedAd(string placement);
    }
}