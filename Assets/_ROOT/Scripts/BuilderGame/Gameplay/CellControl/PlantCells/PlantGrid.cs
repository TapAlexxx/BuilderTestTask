using System;
using System.Collections.Generic;
using System.Linq;
using BuilderGame.Infrastructure.Services.StaticData;
using BuilderGame.StaticData;
using BuilderGame.StaticData.Plants;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.CellControl.PlantCells
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
        private List<PlantCell> harvestableCells;


        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
            Initialize();
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

            harvestableCells = new List<PlantCell>();
        }

        public bool TryGetHarvestable(out PlantCell plantCell)
        {
            plantCell = harvestableCells.FirstOrDefault(x => x.Interactable);
            return plantCell != null;
        }

        private void Start()
        {
            cellsChangedState = 0;
            cellsToChangedState = 0;

            SubscribeOnCellsEvents();
        }

        private void OnDestroy() => 
            UnsubscribeOnCellsEvents();

        private void OnCellGrown(PlantCell plantCell) => 
            harvestableCells.Add(plantCell);

        private void OnHarvested(PlantCell plantCell)
        {
            harvestableCells.Remove(plantCell);
            cellsHarvested++;
            TryReset();
        }

        private void OnReadyToChangeState()
        {
            cellsChangedState++;
            TrySwitchState();
        }

        private void TrySwitchState()
        {
            bool ableToSwitchState = cellsChangedState == cellsToChangedState;
            if (ableToSwitchState)
            {
                cellsChangedState = 0;
                MakeCellsInteractable();
            }
        }

        private void TryReset()
        {
            bool ableToReset = cellsHarvested == cellsToChangedState;
            if (ableToReset)
            {
                foreach (PlantCell cell in cells)
                {
                    cell.StartResetWithDelay(delay);
                }

                cellsHarvested = 0;
                cellsChangedState = 0;
            }
        }

        private void MakeCellsInteractable()
        {
            foreach (PlantCell cell in cells) 
                cell.MakeInteractable();
        }

        private void SubscribeOnCellsEvents()
        {
            foreach (PlantCell cell in cells)
            {
                cell.ReadeToChangState += OnReadyToChangeState;
                cell.Grown += OnCellGrown;
                cell.Harvested += OnHarvested;
                cellsToChangedState++;
            }
        }

        private void UnsubscribeOnCellsEvents()
        {
            foreach (PlantCell cell in cells)
            {
                cell.ReadeToChangState -= OnReadyToChangeState;
                cell.Grown -= OnCellGrown;
                cell.Harvested -= OnHarvested;
            }
        }

#if UNITY_EDITOR
        public void GenerateGrid()
        {
            ClearGrid();
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

        public void ClearGrid()
        {
            foreach (Transform child in transform)
                DestroyImmediate(child.gameObject);
            cells.Clear();
        }
#endif
    }
}