using BuilderGame.Infrastructure.Services.Ads;
using BuilderGame.Infrastructure.Services.Ads.Fake;
using BuilderGame.Infrastructure.Services.Input;
using BuilderGame.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace BuilderGame.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindStaticDataService();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);
            
            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();
            
            Container.Bind<FakeAdsSettings>().FromResources(nameof(FakeAdsSettings)).AsSingle();
            Container.Bind<IAdvertiser>().To<FakeAdvertiser>().AsSingle();
        }
        
        private void BindStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadData();
            Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
        }

        public void Initialize()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
        }
    }
}