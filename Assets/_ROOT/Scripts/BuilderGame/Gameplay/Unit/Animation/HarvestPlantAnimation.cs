using System;
using System.Collections;
using BuilderGame.Gameplay.Plants;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class HarvestPlantAnimation : MonoBehaviour
    {
        private Vector3 initialScale;

        private void Awake()
        {
            initialScale = transform.localScale;
        }

        public void Animate(Plant plant, Action onAnimated)
        {
            StartCoroutine(FlyToTarget(plant.transform, onAnimated));
        }

        private IEnumerator FlyToTarget(Transform movingObject, Action onAnimated)
        {
            Sequence movingObjectSequence = DOTween.Sequence();
            movingObject.transform.DOMove(movingObject.position + Vector3.up * 2, 0.4f);
            movingObjectSequence.Append(movingObject.transform.DOScale(movingObject.localScale * 0.3f, 0.2f));
            movingObjectSequence.Append(movingObject.transform.DOScale(movingObject.localScale * 1f, 0.2f));
            yield return new WaitForSeconds(0.4f);
            movingObject.DOMove(transform.position, 0.15f);
            movingObject.DOScale(Vector3.zero, 0.15f);
            
            Sequence playerSequence = DOTween.Sequence();
            playerSequence.Append(transform.DOScale(initialScale * 1.4f, 0.2f));
            playerSequence.Append(transform.DOScale(initialScale, 0.2f));

            onAnimated?.Invoke();
        }
    }
}