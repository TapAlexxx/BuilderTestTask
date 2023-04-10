using System.Collections.Generic;
using BuilderGame.Infrastructure.Services.StaticData;
using BuilderGame.StaticData;
using BuilderGame.StaticData.Plants;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.CellControl
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private List<PlantCell> cells;

        private IStaticDataService staticDataService;
        private int cellsChangedState;
        private int cellsToChangedState;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
            Initialize();
        }

        private void Initialize()
        {
            PlantStaticData plantStaticData = staticDataService.GetPlantStaticData(PlantType.Wheat);
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
                cellsToChangedState++;
            }
        }

        private void OnDestroy()
        {
            foreach (PlantCell cell in cells) 
                cell.ReadeToChangState -= ValidateGridState;
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

        public void GenerateGrid()
        {
            Clear();
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    GameObject plantCell = Instantiate(cellPrefab);
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
    }
}