using System;
using UnityEngine;

namespace BuilderGame.Gameplay.Player
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