using _ROOT.Scripts.BuilderGame.Infrastructure.Ads;
using _ROOT.Scripts.BuilderGame.Infrastructure.Ads.Fake;
using _ROOT.Scripts.BuilderGame.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace _ROOT.Scripts.BuilderGame.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);
            
            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();

            Container.Bind<FakeAdsSettings>().FromResources(nameof(FakeAdsSettings)).AsSingle();
            Container.Bind<IAdvertiser>().To<FakeAdvertiser>().AsSingle();
        }

        public void Initialize()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
        }
    }
}