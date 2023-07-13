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
            if(index == 0) _playerController.WakeUp();
            Index = index;
        }
    
        public void SpawnPlayerCharacter()
        {
            UIManager.Instance.Show<PlayerPanel>();
            Time.timeScale = 1.0f;
            Player.position = Positions[Index].position;
            Player.localRotation = Positions[Index].localRotation;
        }

        private void Awake()
        {
            SetPositionIndex(5);
            SpawnPlayerCharacter();
        }

        public void StopExternalSound()
        {
            //AudioManager.Instance.ResetSound();
        }
    }
}

