using BuilderGame.Gameplay.CellControl.PlantCells;
using UnityEditor;
using UnityEngine;

namespace _ROOT.Scripts.Editor
{
    [CustomEditor(typeof(PlantGrid))]
    public class GridEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PlantGrid plantGrid = (PlantGrid)target;
            if (GUILayout.Button("GenerateGrid"))
            {
                plantGrid.GenerateGrid();
            }
            if (GUILayout.Button("HandClear"))
                plantGrid.ClearGrid();
        }
    }
}
