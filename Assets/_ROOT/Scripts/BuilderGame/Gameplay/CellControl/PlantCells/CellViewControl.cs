﻿using System.Collections.Generic;
using System.Linq;
using BuilderGame.StaticData;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl.PlantCells
{
    public class CellViewControl : MonoBehaviour
    {
        [SerializeField] private List<PlantCellView> cellViews;
        [SerializeField] private ParticleSystem plowParticle;
        
        public void InitializeCellView(PlantStaticData plantStaticData)
        {
            foreach (PlantCellView cellModel in cellViews)
            {
                Material targetMaterial = plantStaticData.Textures
                    .FirstOrDefault(x => x.plantCellState == cellModel.СellState)
                    ?.Material;

                cellModel.Initialize(targetMaterial);
            }
        }

        public void Show(PlantCellState plantCellState)
        {
            foreach (PlantCellView cellView in cellViews)
                cellView.View.SetActive(cellView.СellState == plantCellState);
        }

        public void AnimatePlow()
        {
            plowParticle.Play();
        }
    }
}