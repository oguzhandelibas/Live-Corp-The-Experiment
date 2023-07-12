using MiniGame.MemoryGame;
using MiniGame.RaidGame;
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
            if (other.TryGetComponent(out MG_Controller mgController) && !mgController.HasTrigger)
            {
                PlayerController.Instance.Lock(mgController.transform, true);
                mgController.StartPathRoutine();
            }
            if (other.TryGetComponent(out RaidControl raidControl) && !raidControl.HasStarted)
            {
                raidControl.StartSlowMotionGame(PlayerController.Instance);
            }
        }
    }
}

