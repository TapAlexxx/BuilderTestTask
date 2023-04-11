using BuilderGame.StaticData.Plants;
using UnityEngine;

namespace BuilderGame.Gameplay.Plants
{
    public class Plant : MonoBehaviour
    {
        [field:SerializeField] public PlantType PlantType { get; private set; }
    }
}