using InfimaGames.LowPolyShooterPack;
using UnityEngine;

namespace Player
{
    public class PlayerController : AbstractSingleton<PlayerController>
    {
        [SerializeField] private Character _character;
        private bool canMove;
        public bool CanMove { get => canMove; set => canMove = value; }
        [SerializeField] private GameObject ammoIndicatorObject;

        private void Start()
        {
            ammoIndicatorObject.SetActive(false);
        }

        public void SetGun()
        {
            _character.HolsterIssue();
            ammoIndicatorObject.SetActive(true);
        }
    }
}

