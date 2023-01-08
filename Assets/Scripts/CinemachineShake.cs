using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class CinemachineShake : MonoBehaviour
    {
        public static CinemachineShake Instance { get; private set; }
        CinemachineVirtualCamera virtualCamera;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannel;
        float timer;
        float timerMax;
        float startIntensity;
        private void Awake()
        {
            Instance = this;
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            cinemachineBasicMultiChannel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            if (timer < timerMax)
            {
                timer += Time.deltaTime;
                cinemachineBasicMultiChannel.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0, timer / timerMax);
            }
        }
        public void ShakeCamera(float intensity, float timer)
        {
            startIntensity = intensity;
            this.timer = 0;
            timerMax = timer;
            cinemachineBasicMultiChannel.m_AmplitudeGain = intensity;
        }
    }
}
