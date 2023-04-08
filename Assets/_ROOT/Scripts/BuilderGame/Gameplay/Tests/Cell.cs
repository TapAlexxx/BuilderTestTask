using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.Tests
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private List<CellView> cellViews;
        
        private float harvestTime;
        private Coroutine growCoroutine;
        
        public bool AbleToSwitchState { get; private set; }
        public PlantState CurrentState { get; private set; }

        public event Action ReadeToChangState;

        private void Start()
        {
            Enter(PlantState.Grass);
            MakeAbleToSwitchState();
        }

        public void MakeAbleToSwitchState()
        {
            AbleToSwitchState = true;
        }

        public void Plow()
        {
            if(!AbleToSwitchState)
                return;
            Enter(PlantState.Plowed);
            ReadeToChangState?.Invoke();
        }

        public void Plant()
        {
            if(!AbleToSwitchState)
                return;
            Enter(PlantState.Planted);
            Grow();
            ReadeToChangState?.Invoke();
        }

        public void Harvest()
        {
            Enter(PlantState.Harvested);
            ReadeToChangState?.Invoke();
        }

        private void Grow()
        {
            growCoroutine = StartCoroutine(GrowPlant());
        }

        private IEnumerator GrowPlant()
        {
            Enter(PlantState.Growing);
            yield return new WaitForSeconds(3f);
            Enter(PlantState.Harvestable);
            AbleToSwitchState = true;
            StopCoroutine(growCoroutine);
        }

        private void Enter(PlantState currentState)
        {
            foreach (CellView cellView in cellViews)
                cellView.View.SetActive(cellView.СellState == currentState);
            CurrentState = currentState;
            AbleToSwitchState = false;
        }
    }
}