using System;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
{
    public abstract class Interactable : MonoBehaviour 
    {
        public abstract event Action StartedInteract;
        public abstract event Action EndedInteract;
    }
}