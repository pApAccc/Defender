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

            transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGenerateData.resourceType.sprite;
            transform.Find("Text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        }
        private void Update()
        {
            barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
        }
    }
}
