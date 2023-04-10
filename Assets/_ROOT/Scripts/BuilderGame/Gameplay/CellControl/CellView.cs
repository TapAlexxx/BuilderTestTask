using System;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl
{
    [Serializable]
    public class CellView
    {
        public GameObject View;
        public Renderer ViewRenderer;
        public CellState СellState;

        public void InitializeView(Material material)
        {
            ViewRenderer.sharedMaterial = material;
        }
    }
}