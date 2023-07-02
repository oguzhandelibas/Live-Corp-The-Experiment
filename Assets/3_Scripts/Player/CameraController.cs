using UnityEngine;
using Cinemachine;
using DG.Tweening;

namespace Player
{
    public class CameraController : AbstractSingleton<CameraController>
    {
        [SerializeField] private float shakeDuration = 0.2f;
        [SerializeField] private float shakeAmplitude = 2.0f;
        [SerializeField] private float frequencyGain = 0.05f;
        
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        private CinemachineBasicMultiChannelPerlin noise;
        private float shakeTimer = 0f;
        
        private void OnEnable()
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            shakeTimer = 0.0f;
            noise.m_AmplitudeGain = 0.0f;
        }
        
        private void Update()
        {
            if (shakeTimer > 0f)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    // Shake süresi tamamlandı, shake'i sıfırla

                    DOTween.To(() => noise.m_AmplitudeGain, x => noise.m_AmplitudeGain = x, 0, 0.1f);
                    DOTween.To(() => noise.m_FrequencyGain, x => noise.m_FrequencyGain = x, 0, 0.1f);
                }
                else
                {
                    // Shake devam ediyor, shake parametrelerini güncelle
                    noise.m_AmplitudeGain = shakeAmplitude;
                }
            }
        }

        public void ShakeCamera()
        {
            shakeTimer = shakeDuration;
            noise.m_AmplitudeGain = shakeAmplitude;
            noise.m_FrequencyGain = frequencyGain;
        }
    }
}
