using UnityEngine;

namespace MiniGame.MemoryGame
{
    public class MG_Input : MonoBehaviour, IInteractable
    {
        private MG_Controller _mgController;

        private void Start()
        {
            _mgController = GetComponentInParent<MG_Controller>();
        }

        public void InteractStart(GameObject interactObject, Transform parent)
        {
            if (!_mgController.CanInteract) return;
            _mgController.Interact(transform.GetSiblingIndex());
        }

        public void OnInteract()
        {
        }

        public void InteractEnd()
        {
        }
    }
}

