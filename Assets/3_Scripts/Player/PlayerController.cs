using DG.Tweening;
using InfimaGames.LowPolyShooterPack;
using NPC;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class PlayerController : AbstractSingleton<PlayerController>, IHealth
    {
        [SerializeField] private Character _character;
        [SerializeField] private CameraLook _cameraLook;
        [SerializeField] private TimeHandler _timeHandler;
        [SerializeField] private Volume volumeObject;
        [SerializeField] private Transform[] hitSens;
        private bool canMove;
        public bool CanMove { get => canMove; set => canMove = value; }
        [SerializeField] private GameObject ammoIndicatorObject;

        private void Start()
        {
            ammoIndicatorObject.SetActive(false);
            volumeObject.enabled = false;
            foreach (var item in hitSens)
            {
                item.transform.localScale = Vector3.zero;
                item.gameObject.SetActive(false);
            }
        }

        #region GUN

        public void SetGun()
        {
            _character.HolsterIssue();
            ammoIndicatorObject.SetActive(true);
        }

        #endregion
        
        #region MOVEMENT & ROTATION

        public void Lock(Transform target)
        {
            _character.isFreeze = true;
            _cameraLook.isFreeze = true;
            Transform playerPoint = target.GetChild(0);
            transform.position = playerPoint.position;
            transform.DORotateQuaternion(Quaternion.Euler(0, target.eulerAngles.y, 0), 0.5f);
            _cameraLook.transform.DOLocalRotate(new Vector3(target.rotation.x, 0,0), 0.5f).OnComplete(UnlockRotation);
        }

        public void UnlockRotation()
        {
            _cameraLook.transform.DOLocalRotate(new Vector3(_cameraLook.transform.localRotation.x, 0, 0), 0.5f);
            _cameraLook.isFreeze = false;
        }
        public void UnlockMovement() => _character.isFreeze = false;

        #endregion

        #region SLOW MOTION

        public void SetSlowMotion()
        {
            _timeHandler.Increase(-0.65f);
        }

        public void SetCurrentTime()
        {
            _timeHandler.Increase(+0.65f);
        }

        #endregion

        public void TakeDamage(Vector3 hitPos)
        {
            CameraController.Instance.ShakeCamera();
            volumeObject.enabled = true;
            volumeObject.weight = 1.0f;
            DOTween.To(() => volumeObject.weight, x => volumeObject.weight = x, 0, 0.5f)
                .SetEase(Ease.Linear);
            Debug.Log("Taked Damege");
        }

        public void GiveDamage()
        {
            foreach (var item in hitSens)
            {
                item.transform.localScale = Vector3.zero;
                item.gameObject.SetActive(true);
                item.DOScale(new Vector3(1, 1, 1), 0.3f).OnComplete(delegate { item.gameObject.SetActive(false); });
            }
        }

        public void Death()
        {
            Debug.Log("Died");
        }
    }
}

