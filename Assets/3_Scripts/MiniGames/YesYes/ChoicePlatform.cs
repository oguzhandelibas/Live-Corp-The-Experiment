using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.YesYes
{
    public class ChoicePlatform : MonoBehaviour, IInteractable
    {
        [SerializeField] private YesYesController _yesController;
        public bool yes = false;
        public void Change()
        {
            _yesController.ChangeMaterial();
        }

        public void InteractStart(GameObject interactObject, Transform parent)
        {
            if (yes)
            {
                _yesController.ActivateGunPanel();
            }
        }

        public void OnInteract()
        {
            
        }

        public void InteractEnd()
        {
            
        }
    }
}

