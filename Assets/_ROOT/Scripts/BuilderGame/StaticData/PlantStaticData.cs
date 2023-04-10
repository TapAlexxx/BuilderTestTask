using System.Collections.Generic;
using BuilderGame.StaticData.Plants;
using UnityEngine;

namespace BuilderGame.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Cell", fileName = "new CellStaticData", order = 0)]
    public class PlantStaticData : ScriptableObject
    {
        public PlantType PlantType;
        public GameObject PlantPrefab;
        
        [Header("Growing settings"), Space(10)]
        public Vector3 TargetPlantScale;
        public Vector2 GrowTime;
        public GrowType GrowType;
        
        [Space(10)]
        public List<ViewTextures> Textures;
    }
}