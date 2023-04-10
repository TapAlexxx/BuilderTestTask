using BuilderGame.StaticData;
using BuilderGame.StaticData.Plants;

namespace BuilderGame.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadData();
        PlantStaticData GetPlantStaticData(PlantType plantType);
    }
}