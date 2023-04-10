using BuilderGame.StaticData;
using UnityEngine;

namespace BuilderGame.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string GameConfigPath = "StaticData/Cells";
        
        private CellStaticData cellStaticData;


        public void LoadData()
        {
            cellStaticData = Resources
                .Load<CellStaticData>(GameConfigPath);
        }
    }
}