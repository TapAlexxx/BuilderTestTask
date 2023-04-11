using System;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl.Plant
{
    [Serializable]
    public class PlantCellView
    {
        public GameObject View;
        public Renderer ViewRenderer;
        public PlantCellState СellState;

        public void Initialize(Material material)
        {
            ViewRenderer.sharedMaterial = material;
        }
    }
}