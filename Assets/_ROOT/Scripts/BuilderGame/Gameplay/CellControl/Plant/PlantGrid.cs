using System.Collections.Generic;
using BuilderGame.Infrastructure.Services.StaticData;
using BuilderGame.StaticData;
using BuilderGame.StaticData.Plants;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.CellControl.Plant
{
    public class PlantGrid : MonoBehaviour
    {
        [SerializeField] private PlantType plantType;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private List<PlantCell> cells;
        [SerializeField] private float delay = 3f;

        private IStaticDataService staticDataService;
        private int cellsChangedState;
        private int cellsHarvested;
        private int cellsToChangedState;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
            Initialize();
        }

        public void GenerateGrid()
        {
            Clear();
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    GameObject plantCell = (GameObject)PrefabUtility.InstantiatePrefab(cellPrefab);
                    plantCell.transform.position = transform.position + new Vector3(i, 0, j);
                    plantCell.name = $"cell {i} {j}";
                    plantCell.transform.parent = transform;
                    cells.Add(plantCell.GetComponent<PlantCell>());
                }
            }
        }

        public void Clear()
        {
            foreach (Transform child in transform)
                DestroyImmediate(child.gameObject);
            cells.Clear();
        }

        private void Initialize()
        {
            PlantStaticData plantStaticData = staticDataService.GetPlantStaticData(plantType);
            foreach (PlantCell plantCell in cells)
            {
                plantCell.Initialize(plantStaticData);
                plantCell.Reset();
                MakeCellsInteractable();
            }
        }

        private void Start()
        {
            cellsChangedState = 0;
            cellsToChangedState = 0;
            foreach (PlantCell cell in cells)
            {
                cell.ReadeToChangState += ValidateGridState;
                cell.Harvested += ValidateReset;
                cellsToChangedState++;
            }
        }

        private void OnDestroy()
        {
            foreach (PlantCell cell in cells)
            {
                cell.ReadeToChangState -= ValidateGridState;
                cell.Harvested -= ValidateReset;
            }
        }

        private void ValidateReset()
        {
            cellsHarvested++;
            bool ableToReset = cellsHarvested == cellsToChangedState;
            if (ableToReset)
            {
                foreach (PlantCell plantCell in cells)
                {
                    plantCell.StartResetWithDelay(delay);
                }

                cellsHarvested = 0;
                cellsChangedState = 0;
            }
        }

        private void ValidateGridState()
        {
            cellsChangedState++;
            bool ableToSwitchState = cellsChangedState == cellsToChangedState;
            if (ableToSwitchState)
            {
                cellsChangedState = 0;
                MakeCellsInteractable();
            }
        }

        private void MakeCellsInteractable()
        {
            foreach (PlantCell cell in cells) 
                cell.MakeInteractable();
        }
    }
}