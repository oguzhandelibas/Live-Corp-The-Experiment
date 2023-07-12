using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        [Header("SpawnPoses")] [SerializeField]
        private Transform[] Positions;

        private int Index;

        public void SetPositionIndex(int index)
        {
            if(index == 0) PlayerController.Instance.WakeUp();
            Index = index;
        }
    
        public void SpawnPlayerCharacter()
        {
            Player.position = Positions[Index].position;
            Player.localRotation = Positions[Index].localRotation;
        }

        private void Awake()
        {
            SetPositionIndex(2);
            SpawnPlayerCharacter();
        }
    }
}

