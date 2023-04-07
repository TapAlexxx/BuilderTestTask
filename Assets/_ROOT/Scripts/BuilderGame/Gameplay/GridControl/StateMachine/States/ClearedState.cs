using System;
using UnityEngine;

namespace BuilderGame.Gameplay.GridControl.StateMachine.States
{
    public class ClearedState : CellState
    {
        public override event Action ReadyToNextState;

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
    }
}