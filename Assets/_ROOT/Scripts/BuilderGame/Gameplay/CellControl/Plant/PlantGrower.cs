using BuilderGame.StaticData;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.CellControl.Plant
{
    public class PlantGrower : MonoBehaviour
    {
        [SerializeField] private Transform plantSpawnPoint;

        private GameObject plant;
        private Vector3 targetPlantScale;

        public void Initialize(PlantStaticData plantStaticData)
        {
            plant = Instantiate(plantStaticData.PlantPrefab, plantSpawnPoint.position, Quaternion.identity);
            plant.transform.parent = transform;
            plant.transform.localScale = new Vector3(targetPlantScale.x, 0,targetPlantScale.z);
            targetPlantScale = plantStaticData.TargetPlantScale;
        }

        public void Reset()
        {
            plant.transform.localScale = new Vector3(targetPlantScale.x, 0,targetPlantScale.z);
        }

        public void LinearGrow(float growTime)
        {
            plant.transform.DOScale(targetPlantScale, growTime);
        }

        public void BounceGrow(float bounceTime)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(plant.transform.DOScale(targetPlantScale * 1.2f, bounceTime/2));
            sequence.Append(plant.transform.DOScale(targetPlantScale, bounceTime/2));
        }

        public void Scale(Vector3 targetScale)
        {
            plant.transform.DOScale(targetScale, 0.2f);
        }
    }
}