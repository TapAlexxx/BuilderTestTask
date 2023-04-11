using System.Collections.Generic;
using System.Linq;
using BuilderGame.StaticData;
using BuilderGame.StaticData.Plants;
using UnityEngine;

namespace BuilderGame.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string GameConfigPath = "StaticData/Plants";
        
        private List<PlantStaticData> plantStaticData;


        public void LoadData()
        {
            plantStaticData = Resources
                .LoadAll<PlantStaticData>(GameConfigPath)
                .ToList();
        }

        public PlantStaticData GetPlantStaticData(PlantType plantType) => 
            plantStaticData.FirstOrDefault(x => x.PlantType == plantType);
    }
}