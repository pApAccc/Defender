using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ResourceNearbyOverlay : MonoBehaviour
    {
        ResourceGenerateData resourceGenerateData;
        private void Awake()
        {
            Hide();
        }
        private void Update()
        {
            int nearbyResourceAmount = ResourceGenerater.GetNearbyResourceamount(resourceGenerateData, transform.position - transform.localPosition);
            float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGenerateData.maxResourceAmount * 100);

            transform.Find("Text").GetComponent<TextMeshPro>().text = percent + "%";
        }
        public void Show(ResourceGenerateData resourceGenerateData)
        {
            this.resourceGenerateData = resourceGenerateData;
            gameObject.SetActive(true);

            transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGenerateData.resourceType.sprite;
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
