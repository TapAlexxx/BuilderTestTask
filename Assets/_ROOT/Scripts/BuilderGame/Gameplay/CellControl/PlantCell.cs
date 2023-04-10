using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BuilderGame.StaticData;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BuilderGame.Gameplay.CellControl
{
    public class PlantCell : MonoBehaviour
    {
        [SerializeField] private List<CellView> cellViews;
        [SerializeField] private Transform plantSpawnPoint;

        private GameObject plant;
        private Coroutine growCoroutine;
        private Vector3 targetPlantScale;
        private Vector2 growTimeRange;

        public bool Interactable { get; private set; }
        public CellState CurrentState { get; private set; }

        public event Action ReadeToChangState;


        public void Initialize(PlantStaticData plantStaticData)
        {
            foreach (CellView cellView in cellViews)
            {
                Material targetMaterial = plantStaticData.ViewTextures.FirstOrDefault(x => x.cellState == cellView.СellState).Material;
                cellView.InitializeView(targetMaterial);
            }

            plant = Instantiate(plantStaticData.PlantPrefab, plantSpawnPoint.position, Quaternion.identity);
            plant.transform.parent = transform;
            plant.transform.localScale = new Vector3(targetPlantScale.x, 0,targetPlantScale.z);
            targetPlantScale = plantStaticData.TargetPlantScale;
            growTimeRange = plantStaticData.GrowTime;
        }

        public void Plow()
        {
            DisableInteraction();
            SwitchState(CellState.Plowed);
            ShowView(CellState.Plowed);
            ReadeToChangState?.Invoke();
        }

        public void Plant()
        {
            DisableInteraction();
            ShowView(CellState.Planted);
            StartGrow();
        }

        private void StartGrow() => 
            growCoroutine = StartCoroutine(GrowPlant());

        public void Harvest()
        {
            DisableInteraction();
            SwitchState(CellState.Harvested);
            ShowView(CellState.Harvested);
            plant.transform.DOScale(Vector3.zero, 0.3f);
        }

        private IEnumerator GrowPlant()
        {
            float growTime = Random.Range(growTimeRange.x, growTimeRange.y);

            //LinearGrow(growTime);
            yield return new WaitForSeconds(growTime);
            
            BounceGrow();
            yield return new WaitForSeconds(0.6f);

            SwitchState(CellState.Grown);
            MakeInteractable();
            StopCoroutine(growCoroutine);
        }

        private void LinearGrow(float growTime)
        {
            plant.transform.DOScale(targetPlantScale, growTime);
        }
        
        private void BounceGrow()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(plant.transform.DOScale(targetPlantScale * 1.2f, 0.3f));
            sequence.Append(plant.transform.DOScale(targetPlantScale, 0.3f));
        }

        public void MakeInteractable() => 
            Interactable = true;

        private void DisableInteraction() => 
            Interactable = false;

        public void Reset()
        {
            SwitchState(CellState.Grass);
            ShowView(CellState.Grass);
            plant.transform.localScale = new Vector3(targetPlantScale.x, 0,targetPlantScale.z);
            ReadeToChangState?.Invoke();
        }

        private void ShowView(CellState cellState)
        {
            foreach (CellView cellView in cellViews)
                cellView.View.SetActive(cellView.СellState == cellState);
        }

        private void SwitchState(CellState cellState) => 
            CurrentState = cellState;
    }
}