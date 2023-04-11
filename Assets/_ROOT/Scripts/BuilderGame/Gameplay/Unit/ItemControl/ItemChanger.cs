using System;
using System.Collections.Generic;
using System.Linq;
using BuilderGame.Gameplay.Unit.CellInteraction;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.ItemControl
{
    public class ItemChanger : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        [SerializeField] private UnitPlower unitPlower;
        [SerializeField] private UnitPlanter unitPlanter;
        
        private Item currentItem;

        private void OnValidate()
        {
            unitPlower = GetComponentInChildren<UnitPlower>();
            unitPlanter = GetComponentInChildren<UnitPlanter>();
        }
        
        private void Start()
        {
            currentItem = null;

            unitPlower.StartedInteract += TakePlowing;
            unitPlanter.StartedInteract += TakePlanting;
            
            unitPlower.EndedInteract += HideIfExist;
            unitPlanter.EndedInteract += HideIfExist;
        }

        private void OnDestroy()
        {
            unitPlower.StartedInteract -= TakePlowing;
            unitPlanter.StartedInteract -= TakePlanting;

            unitPlower.EndedInteract -= HideIfExist;
            unitPlanter.EndedInteract -= HideIfExist;
        }

        private void TakePlowing() => 
            Take(ItemType.Plowing);

        private void TakePlanting() => 
            Take(ItemType.Planting);

        private void Take(ItemType itemType)
        {
            HideIfExist();
            currentItem = items.FirstOrDefault(x => x.ItemType == itemType);
            if (currentItem == null)
                throw new NullReferenceException("no item to grab");
            
            currentItem.ItebObject.transform.DOScale(currentItem.TargetScale, currentItem.AppearingTime);
        }

        private void HideIfExist()
        {
            if(currentItem == null)
                return;
            currentItem.ItebObject.transform.DOScale(Vector3.zero, currentItem.AppearingTime);
            currentItem = null;
        }
    }
}