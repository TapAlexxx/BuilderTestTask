using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Cell", fileName = "new CellStaticData", order = 0)]
    public class CellStaticData : ScriptableObject
    {
        public PlantType PlantType;
        public GameObject PlantPrefab;
        public List<ViewTextures> ViewTextures;
    }
}