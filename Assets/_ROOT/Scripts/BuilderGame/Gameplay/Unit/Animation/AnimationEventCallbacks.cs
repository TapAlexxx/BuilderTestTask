using System;
using JetBrains.Annotations;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class AnimationEventCallbacks : MonoBehaviour
    {
        public event Action Plowed;
        public event Action Planted;

        [UsedImplicitly]
        public void PlowEnded()
        {
            Plowed?.Invoke();
        }
        
        [UsedImplicitly]
        public void PlantEnded()
        {
            Planted?.Invoke();
        }
    }
}