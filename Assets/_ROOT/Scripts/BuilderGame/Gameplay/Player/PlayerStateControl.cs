using System.Collections.Generic;
using System.Linq;
using BuilderGame.Gameplay.Player.Movement;
using BuilderGame.Gameplay.Unit.CellInteraction;
using UnityEngine;

namespace BuilderGame.Gameplay.Player
{
    public class PlayerStateControl : MonoBehaviour
    {
        [SerializeField] private List<Interactable> interactables;
        [SerializeField] private PlayerMovementControl playerMovementControl;

        public bool Interacting { get; private set; }

        private void OnValidate()
        {
            interactables = GetComponentsInChildren<Interactable>().ToList();
            playerMovementControl = GetComponentInChildren<PlayerMovementControl>();
        }
        
        private void Start()
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.StartedInteract += OnStartedInteract;
                interactable.EndedInteract   += OnEndedInteract;
            }
        }

        private void OnDestroy()
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.StartedInteract -= OnStartedInteract;
                interactable.EndedInteract -= OnEndedInteract;
            }
        }

        private void OnStartedInteract()
        {
            playerMovementControl.Disable();
            Interacting = true;
        }

        private void OnEndedInteract()
        {
            playerMovementControl.Activate();
            Interacting = false;
        }
    }
}