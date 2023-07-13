using System;
using DG.Tweening;
using InfimaGames.LowPolyShooterPack;
using NPC;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : AbstractSingleton<PlayerController>, IHealth
    {
        [Header("Control")] public PlayerManager PlayerManager;
        public Camera PlayerCam;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Character _character;
        [SerializeField] private CameraLook _cameraLook;
        [SerializeField] private TimeHandler _timeHandler;
        [SerializeField] private Volume takeDamageVolume;
        [SerializeField] private Volume deadVolume;
        [SerializeField] private Transform[] hitSens;
        [SerializeField] private Image Crosshair;

        [Header("UI Panels")] 
        [SerializeField] private GameObject TutorialPanel;
        [SerializeField] private GameObject MovementIndicator;
        [SerializeField] private GameObject CollectIndicator;
        [SerializeField] private GameObject TimerIndicator;
        [SerializeField] private GameObject ConsolePanel;
        
        private int Health;
        private bool alive;
        private bool canMove;
        public bool CanMove { get => canMove; set => canMove = value; }
        [SerializeField] private GameObject ammoIndicatorObject;

        private void Start()
        {
            alive = true;
            Health = 100;
            
            TutorialPanelActiveness(false);
            TutorialMovementActiveness(false);
            TutorialCollectActiveness(false);
            TimerPanelActiveness(false);
            ConsolePanelActiveness(false);
            
            Crosshair.gameObject.SetActive(true);
            ammoIndicatorObject.SetActive(false);
            takeDamageVolume.enabled = false;
            deadVolume.enabled = false;
            ResetHitSens();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ConsolePanelActiveness(!ConsolePanel.activeSelf);
            }
        }

        #region GUN

        public void SetGun()
        {
            _character.HolsterIssue();
            ammoIndicatorObject.SetActive(true);
        }

        public void TakeAmmunation(int count)
        {
            _weapon.SetBackupAmmunition(count);
        }

        #endregion
        
        #region MOVEMENT & ROTATION

        public void Lock(Transform target, bool unlockAfter = false)
        {
            _character.isFreeze = true;
            _cameraLook.isFreeze = true;
            Transform playerPoint = target.GetChild(0);
            transform.position = playerPoint.position;
            transform.DORotateQuaternion(Quaternion.Euler(0, target.eulerAngles.y, 0), 0.5f);
            if (unlockAfter)
                _cameraLook.transform.DOLocalRotate(new Vector3(target.rotation.x, 0, 0), 0.5f)
                    .OnComplete(UnlockRotation);
            else
                _cameraLook.transform.DOLocalRotate(new Vector3(target.rotation.x, 0, 0), 0.5f);
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

        #region UI

        public void SetCrosshairColor(Color crosshairColor)
        {
            Crosshair.color = crosshairColor;
        }

        public void TutorialPanelActiveness(bool panel)
        {
            TutorialPanel.SetActive(panel);
        }

        public void TutorialMovementActiveness(bool movement)
        {
            MovementIndicator.SetActive(movement);
        }
        
        public void TutorialCollectActiveness(bool collect)
        {
            CollectIndicator.SetActive(collect);
        }

        public void TimerPanelActiveness(bool timer)
        {
            TimerIndicator.SetActive(timer);
        }
        
        public void ConsolePanelActiveness(bool console)
        {
            ConsolePanel.SetActive(console);
        }
        
        #endregion

        #region DAMAGE SYSTEM

        public void TakeDamage(Vector3 hitPos)
        {
            if(!alive) return;
            CameraController.Instance.ShakeCamera();
            takeDamageVolume.enabled = true;
            takeDamageVolume.weight = 1.0f;
            DOTween.To(() => takeDamageVolume.weight, x => takeDamageVolume.weight = x, 0, 0.5f)
                .SetEase(Ease.Linear);
            Health -= 25;
            if(Health<=0) Death();
        }
        public void GiveDamage()
        {
            foreach (var item in hitSens)
            {
                item.transform.localScale = Vector3.zero;
                item.gameObject.SetActive(true);
                item.DOScale(new Vector3(1, 1, 1), 0.3f);
                Debug.Log("a");
            }
            Debug.Log("b");
            ResetHitSens();
        }

        public void ResetHitSens()
        {
            foreach (var item in hitSens)
            {
                item.transform.localScale = Vector3.zero;
                item.gameObject.SetActive(false);
            }
        }
        
        public void Death()
        {
            SetSlowMotion();
            Lock(transform,false);
            Crosshair.gameObject.SetActive(false);
            alive = false;
            deadVolume.enabled = true;
            deadVolume.weight = 0.0f;
            _character.UnlockCursor();
            DOTween.To(() => deadVolume.weight, x => deadVolume.weight = x, 1, 1.5f)
                .SetEase(Ease.Linear).OnComplete(delegate { UIManager.Instance.Show<LosePanel>(); });
        }

        #endregion
        
        public void WakeUp()
        {
            Crosshair.gameObject.SetActive(false);
            alive = true;
            deadVolume.enabled = true;
            deadVolume.weight = 1.0f;
            DOTween.To(() => deadVolume.weight, x => deadVolume.weight = x, 0, 2.5f)
                .SetEase(Ease.Linear).OnComplete(delegate { Crosshair.gameObject.SetActive(true); });
            
        }
        
    }
}

