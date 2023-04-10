using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.Tests
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private List<Cell> cells;
        
        private int cellsChangedState;
        private int cellsToChangedState;

        private void Start()
        {
            cellsChangedState = 0;
            cellsToChangedState = 0;
            foreach (Cell cell in cells)
            {
                cell.ReadeToChangState += ValidateGridState;
                cellsToChangedState++;
            }
        }

        private void OnDestroy()
        {
            foreach (Cell cell in cells) 
                cell.ReadeToChangState -= ValidateGridState;
        }

        private void ValidateGridState()
        {
            cellsChangedState++;
            bool ableToSwitchState = cellsChangedState == cellsToChangedState;
            if (ableToSwitchState)
            {
                EnterNextState();
                cellsChangedState = 0;
            }
        }

        private void EnterNextState()
        {
            foreach (Cell cell in cells) 
                cell.MakeAbleToSwitchState();
        }

        public void GenerateGrid()
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    Cell cell = new GameObject().AddComponent<Cell>();
                    cell.transform.position = transform.position + new Vector3(i, 0, j);
                    cell.name = $"cell {i} {j}";
                    cell.transform.parent = transform;
                }
            }
        }
    }
}