using BuilderGame.Gameplay.CellControl.PlantCells;
using UnityEngine;

namespace BuilderGame.AIControl
{
    public class AIInitializer : MonoBehaviour
    {
        [SerializeField] private AIMovementControl aiMovementControl;
        
        private void OnValidate()
        {
            aiMovementControl = GetComponentInChildren<AIMovementControl>();
        }

        public void Initialize(PlantGrid plantGrid)
        {
            aiMovementControl.Initialize(plantGrid);
        }
    }
}