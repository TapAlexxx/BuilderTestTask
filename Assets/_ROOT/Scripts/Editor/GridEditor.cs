using UnityEditor;
using UnityEngine;
using Grid = BuilderGame.Gameplay.CellControl.Grid;

namespace _ROOT.Scripts.Editor
{
    [CustomEditor(typeof(Grid))]
    public class GridEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Grid grid = (Grid)target;
            if (GUILayout.Button("GenerateGrid"))
            {
                grid.GenerateGrid();
            }
            if (GUILayout.Button("HandClear"))
                grid.Clear();
        }
    }
}
