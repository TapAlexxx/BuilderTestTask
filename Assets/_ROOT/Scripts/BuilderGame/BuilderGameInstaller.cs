﻿using Zenject;

namespace _ROOT.Scripts.BuilderGame
{
    public class BuilderGameInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuilderGameInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            
        }
    }
}