using BuilderGame.Gameplay.Plants;
using BuilderGame.StaticData;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl.PlantCells
{
    public class PlantGrower : MonoBehaviour
    {
        [SerializeField] private Transform plantSpawnPoint;

        private Vector3 targetPlantScale;
        public Plant Plant { get; private set; }

        public void Initialize(PlantStaticData plantStaticData)
        {
            targetPlantScale = plantStaticData.TargetPlantScale;
            Plant = SetupPlant(plantStaticData);
        }

        private Plant SetupPlant(PlantStaticData plantStaticData)
        {
            GameObject plantObject = Instantiate(plantStaticData.PlantPrefab.gameObject, plantSpawnPoint.position, Quaternion.identity);
            plantObject.transform.parent = transform;
            plantObject.transform.localScale = new Vector3(targetPlantScale.x, 0, targetPlantScale.z);
            return plantObject.GetComponent<Plant>();
        }

        public void Reset()
        {
            Plant.transform.localScale = new Vector3(targetPlantScale.x, 0,targetPlantScale.z);
            Plant.transform.position = plantSpawnPoint.position;
        }

        public void LinearGrow(float growTime)
        {
            Plant.transform.DOScale(targetPlantScale, growTime);
        }

        public void BounceGrow(float bounceTime)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(Plant.transform.DOScale(targetPlantScale * 1.2f, bounceTime/2));
            sequence.Append(Plant.transform.DOScale(targetPlantScale, bounceTime/2));
        }

        public void Scale(Vector3 targetScale)
        {
            Plant.transform.DOScale(targetScale, 0.2f);
        }
    }
}