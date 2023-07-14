using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Transform Player;
        [Header("SpawnPoses")] [SerializeField]
        private Transform[] Positions;

        public int Index;

        public void SetPositionIndex(int index)
        {
            
            Index = index;
        }
    
        public void SpawnPlayerCharacter()
        {
            UIManager.Instance.Show<PlayerPanel>();
            Time.timeScale = 1.0f;
            Player.position = Positions[Index].position;
            Player.localRotation = Positions[Index].localRotation;
            if(Index == 0) _playerController.WakeUp();
        }

        private void Awake()
        {
            SetPositionIndex(0);
            SpawnPlayerCharacter();
        }

        public void StopExternalSound()
        {
            //AudioManager.Instance.ResetSound();
        }
    }
}

