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
        public Vector3 TargetPlantScale;
        public List<ViewTextures> ViewTextures;
        public Vector2 GrowTime;
    }
}