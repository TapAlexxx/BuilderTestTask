using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.GridControl.StateMachine.States
{
    public class CellStateMachine : MonoBehaviour
    {
        [SerializeField] private List<CellState> states;
        [SerializeField] private int initialStateId;

        private CellState currentState;
        private int currentStateId;

        private void Start()
        {
            SwitchToState(initialStateId);
            currentState.Enter();
            currentState.ReadyToNextState += TrySwitchToNextState;
        }

        private void TrySwitchToNextState()
        {
            currentState.Exit();
            currentState.ReadyToNextState -= TrySwitchToNextState;

            int nextStateId = GetNextStateId(currentStateId);
            SwitchToState(nextStateId);
            currentState.Enter();
            currentState.ReadyToNextState += TrySwitchToNextState;
        }

        private void SwitchToState(int stateId)
        {
            if (stateId < 0 || stateId >= states.Count)
                throw new ArgumentOutOfRangeException($"invalid stateId - {stateId}");
            
            currentStateId = stateId;
            currentState = states[currentStateId];
        }

        private int GetNextStateId(int currentCellStateId) =>
            currentCellStateId + 1 >= states.Count ? 0 : currentCellStateId + 1;
    }
}