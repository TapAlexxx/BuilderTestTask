using System;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.ItemControl
{
    [Serializable]
    public class Item
    {
        public ItemType ItemType;
        public GameObject ItebObject;
        public Vector3 TargetScale;
        public float AppearingTime;
    }
}