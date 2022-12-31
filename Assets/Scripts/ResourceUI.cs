using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 根据模板生成UI
/// </summary>
namespace ns
{
    public class ResourceUI : MonoBehaviour
    {
        ResourceTypeListSO resourceTypeListSO;
        Dictionary<ResourceTypeSO, Transform> resourceTypeTF;
        void Awake()
        {
            resourceTypeTF = new Dictionary<ResourceTypeSO, Transform>();
            resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            Transform resourceTemplate = transform.Find("ResourceTemplate");
            resourceTemplate.gameObject.SetActive(false);

            int index = 0;
            foreach (var resourceType in resourceTypeListSO.list)
            {
                Transform resourceTransform = Instantiate(resourceTemplate, transform);
                resourceTypeTF[resourceType] = resourceTransform;
                resourceTransform.gameObject.SetActive(true);

                float offset = -160;
                resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);
                resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

                index++;
            }
        }
        private void Start()
        {
            ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
            UpdateResourceAmount();
        }

        private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
        {
            UpdateResourceAmount();
        }

        private void UpdateResourceAmount()
        {
            foreach (var resourceType in resourceTypeListSO.list)
            {
                Transform resourceTransform = resourceTypeTF[resourceType];
                resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().text = ResourceManager.Instance
                                                                            .GetReourceAmount(resourceType).ToString();
            }

        }
    }
}
