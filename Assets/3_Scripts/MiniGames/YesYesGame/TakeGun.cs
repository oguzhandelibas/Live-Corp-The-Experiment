using UnityEngine;


namespace MiniGame.YesYes
{
    public class TakeGun : MonoBehaviour, IInteractable
    {
        [SerializeField] private YesYesController _yesController;

        public void InteractStart(GameObject interactObject, Transform parent)
        {
            _yesController.DeactivateGunPlatform();
        }

        public void OnInteract()
        {
        }

        public void InteractEnd()
        {
        }
    }
}