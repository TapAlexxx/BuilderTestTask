using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Player
{
    public class ItemChanger : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        
        private Item currentItem;

        private void Start()
        {
            currentItem = null;
        }

        public void Take(ItemType itemType)
        {
            HideIfExist();
            currentItem = items.FirstOrDefault(x => x.ItemType == itemType);
            if (currentItem == null)
                throw new NullReferenceException("no item to grab");
            
            currentItem.ItebObject.transform.DOScale(currentItem.TargetScale, currentItem.AppearingTime);
        }

        public void HideIfExist()
        {
            if(currentItem == null)
                return;
            currentItem.ItebObject.transform.DOScale(Vector3.zero, currentItem.AppearingTime);
            currentItem = null;
        }
    }
}