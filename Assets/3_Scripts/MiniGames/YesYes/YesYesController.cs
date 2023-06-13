using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InfimaGames.LowPolyShooterPack;

namespace MiniGame.YesYes
{
    public class YesYesController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [Header("Portal")]
        [SerializeField] private GameObject portal;
        
        [Header("Platforms")]
        [SerializeField] private ChoicePlatform yesPlatform;
        [SerializeField] private ChoicePlatform noPlatform;
        [SerializeField] private Transform gunPlatform;

        [Header("Materials")]
        public Material yesMaterial;
        public Material noMaterial;

        private BoxCollider _boxCollider;
        private MeshRenderer yesPlatformRenderer, noPlatformRenderer;
        private bool reverse = false;
        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
            yesPlatformRenderer = yesPlatform.GetComponent<MeshRenderer>();
            noPlatformRenderer = noPlatform.GetComponent<MeshRenderer>();

            yesPlatformRenderer.material = yesMaterial;
            noPlatformRenderer.material = noMaterial;

            yesPlatform.transform.localPosition = new Vector3(0.75f, -0.5f, 0.0f);
            noPlatform.transform.localPosition = new Vector3(-0.75f, -0.5f, 0.0f);
            gunPlatform.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
            
            yesPlatform.gameObject.SetActive(false);
            noPlatform.gameObject.SetActive(false);
            gunPlatform.gameObject.SetActive(false);
        }

        
        public void ChangeMaterial()
        {
            if (!reverse)
            {
                yesPlatformRenderer.material = noMaterial;
                noPlatformRenderer.material = yesMaterial;
                reverse = true;
                yesPlatform.yes = false;
                noPlatform.yes = true;
            }
            else
            {
                yesPlatformRenderer.material = yesMaterial;
                noPlatformRenderer.material = noMaterial;
                reverse = false;
                noPlatform.yes = false;
                yesPlatform.yes = true;
            }
            
        }

        public void ActivatePortal()
        {
            portal.gameObject.SetActive(true);
        }
        
        public void ActivateChoosePlatform()
        {
            _boxCollider.enabled = false;
            yesPlatform.gameObject.SetActive(true);
            noPlatform.gameObject.SetActive(true);
            Vector3 yesTargetPos = new Vector3(0.75f, 1, 0);
            Vector3 noTargetPos = new Vector3(-0.75f, 1, 0);
            yesPlatform.transform.DOLocalMove(yesTargetPos, 1.0f).SetEase(Ease.Linear);
            noPlatform.transform.DOLocalMove(noTargetPos, 1.0f).SetEase(Ease.Linear);
        }

        private void DeactivateChoosePlatform()
        {
            yesPlatform.gameObject.SetActive(false);
            noPlatform.gameObject.SetActive(false);
        }

        public void ActivateGunPanel()
        {
            gunPlatform.gameObject.SetActive(true);
            DeactivateChoosePlatform();
            Vector3 gunPlatformTargetPos = new Vector3(0.0f, 1.0f, 0.0f);
            gunPlatform.DOLocalMove(gunPlatformTargetPos, 1.0f).SetEase(Ease.Linear);
        }

        public void DeactivateGunPlatform()
        {
            _playerController.SetGun();
            Vector3 gunPlatformTargetPos = new Vector3(0.0f, -0.5f, 0.0f);
            gunPlatform.DOLocalMove(gunPlatformTargetPos, 0.5f).OnComplete(Deactivate);
            
        }

        private void Deactivate()
        {
            gunPlatform.gameObject.SetActive(false);
        }
    }
}

