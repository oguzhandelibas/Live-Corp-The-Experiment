using MiniGame.YesYes;
using UnityEngine;

namespace Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out YesYesController yesController))
            {
                yesController.ActivateChoosePlatform();
            }
        }
    }
}

