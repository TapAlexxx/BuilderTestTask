using System;
using System.Collections;
using BuilderGame.StaticData;
using BuilderGame.StaticData.Plants;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BuilderGame.Gameplay.CellControl.Plant
{
    public class PlantCell : MonoBehaviour
    {
        [SerializeField] private PlantGrower plantGrower;
        [SerializeField] private CellViewControl cellViewControl;
        
        private Coroutine growCoroutine;
        private Coroutine resetCoroutine;
        private Vector2 growTimeRange;
        private GrowType growType;
        
        public bool Interactable { get; private set; }
        public PlantCellState CurrentState { get; private set; }

        public event Action ReadeToChangState;
        public event Action Harvested;
        public event Action<PlantCell> BecameInteractable;


        public void Initialize(PlantStaticData plantStaticData)
        {
            cellViewControl.InitializeCellView(plantStaticData);
            plantGrower.Initialize(plantStaticData);
            
            growTimeRange = plantStaticData.GrowTime;
            growType = plantStaticData.GrowType;
        }

        public void MakeInteractable()
        {
            Interactable = true;
            BecameInteractable?.Invoke(this);
        }

        public void Plow()
        {
            cellViewControl.AnimatePlow();
            DisableInteraction();
            SwitchState(PlantCellState.Plowed);
            cellViewControl.Show(PlantCellState.Plowed);
            ReadeToChangState?.Invoke();
        }

        public void Plant()
        {
            DisableInteraction();
            cellViewControl.Show(PlantCellState.Planted);
            StartGrow();
        }

        public void Harvest()
        {
            DisableInteraction();
            SwitchState(PlantCellState.Harvested);
            cellViewControl.Show(PlantCellState.Harvested);
            plantGrower.Scale(Vector3.zero);
            Harvested?.Invoke();
        }

        public void StartResetWithDelay(float delay)
        {
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
            }
            resetCoroutine = StartCoroutine(ResetWithDelay(delay));
        }

        public void Reset()
        {
            SwitchState(PlantCellState.Grass);
            cellViewControl.Show(PlantCellState.Grass);
            plantGrower.Reset();
            ReadeToChangState?.Invoke();
        }

        private void DisableInteraction() => 
            Interactable = false;

        private void StartGrow()
        {
            float growTime = Random.Range(growTimeRange.x, growTimeRange.y);
            growCoroutine = growType switch
            {
                GrowType.Linear => StartCoroutine(GrowLinear(growTime)),
                GrowType.Bounce => StartCoroutine(GrowBounce(growTime)),
                _ => growCoroutine
            };
        }

        private IEnumerator GrowBounce(float growTime)
        {
            yield return new WaitForSeconds(growTime);
            plantGrower.BounceGrow(Constants.AnimationBounceTime);
            yield return new WaitForSeconds(Constants.AnimationBounceTime);

            SwitchToGrown();
            StopCoroutine(growCoroutine);
        }

        private IEnumerator GrowLinear(float growTime)
        {
            plantGrower.LinearGrow(growTime);
            yield return new WaitForSeconds(growTime);

            SwitchToGrown();
            StopCoroutine(growCoroutine);
        }

        private void SwitchToGrown()
        {
            SwitchState(PlantCellState.Grown);
            cellViewControl.Show(PlantCellState.Grown);
            MakeInteractable();
        }

        private void SwitchState(PlantCellState plantCellState) => 
            CurrentState = plantCellState;

        private IEnumerator ResetWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Reset();
        }
    }
}