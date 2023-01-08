using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] Gradient gradient;
        Light2D light2D;
        float dayTime;
        [SerializeField] float dayTimeSpeed = 1f;
        private void Awake()
        {
            light2D = GetComponent<Light2D>();
        }
        private void Update()
        {
            dayTime += Time.deltaTime * dayTimeSpeed;
            light2D.color = gradient.Evaluate(dayTime % 1f);
        }
    }
}
