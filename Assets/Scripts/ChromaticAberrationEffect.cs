using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ChromaticAberrationEffect : MonoBehaviour
    {
        public static ChromaticAberrationEffect Instance { get; private set; }
        private Volume volume;
        private void Awake()
        {
            Instance = this;
            volume = GetComponent<Volume>();
        }
        private void Update()
        {
            if (volume.weight > 0)
            {
                float dexreaseSpeed = 1f;
                volume.weight -= dexreaseSpeed * Time.deltaTime;
            }
        }
        public void SetVolumeWeight(float weight)
        {
            volume.weight = weight;
        }
    }
}
