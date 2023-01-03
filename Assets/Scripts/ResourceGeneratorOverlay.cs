using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ResourceGeneratorOverlay : MonoBehaviour
    {
        [SerializeField] ResourceGenerater resourceGenerator;
        Transform barTransform;
        private void Start()
        {
            ResourceGenerateData resourceGenerateData = resourceGenerator.GetResourceGeneratorData();
            barTransform = transform.Find("Bar");

            barTransform.localScale = new Vector3(1, 1, 1);
            transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGenerateData.resourceType.sprite;
            transform.Find("Text").GetComponent<TextMeshPro>().SetText(
               resourceGenerator.enabled ? resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1") : "0");
        }
        private void Update()
        {
            barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
        }
    }
}
