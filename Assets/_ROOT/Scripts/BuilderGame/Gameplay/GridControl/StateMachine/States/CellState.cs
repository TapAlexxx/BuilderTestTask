using System;
using UnityEngine;

namespace BuilderGame.Gameplay.GridControl.StateMachine.States
{
    public abstract class CellState : MonoBehaviour
    {
        public abstract event Action ReadyToNextState;
        public abstract void Enter();
        public abstract void Exit();
    }
}