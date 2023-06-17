using MiniGame.MemoryGame;
using MiniGame.YesYes;
using UnityEngine;

namespace Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out YesYesController yesController))
            {
                yesController.ActivateChoosePlatform();
            }
            if (other.TryGetComponent(out MG_Controller mgController) && !mgController.HasTrigger)
            {
                _playerController.Lock(mgController.transform);
                mgController.StartPathRoutine();
            }
        }
    }
}

