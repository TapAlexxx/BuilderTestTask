using System.Collections.Generic;
using BuilderGame.Gameplay.Plants;
using BuilderGame.StaticData.Plants;
using UnityEngine;

namespace BuilderGame.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Cell", fileName = "new CellStaticData", order = 0)]
    public class PlantStaticData : ScriptableObject
    {
        public PlantType PlantType;
        public Plant PlantPrefab;
        
        [Header("Growing settings"), Space(10)]
        public Vector3 TargetPlantScale;
        public Vector2 GrowTime;
        public GrowType GrowType;
        
        [Header("PlantCell settings"), Space(10)]
        public List<ViewConfigs> Textures;
    }
}