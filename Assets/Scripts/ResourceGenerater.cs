using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ResourceGenerater : MonoBehaviour
    {
        ResourceGenerateData resourceGenerateData;
        float timer;
        //过多久增加一次资源
        float timerMax;

        public static int GetNearbyResourceamount(ResourceGenerateData resourceGenerateData, Vector3 position)
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGenerateData.resourceDetectionRadius);

            int nearbyResourceAmount = 0;
            foreach (Collider2D collider2d in collider2DArray)
            {
                ResourceNode resourceNode = collider2d.GetComponent<ResourceNode>();
                if (resourceNode != null)
                    if (resourceNode.resourceType == resourceGenerateData.resourceType)
                    {
                        nearbyResourceAmount++;
                    }
            }

            nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGenerateData.maxResourceAmount);
            return nearbyResourceAmount;
        }
        private void Awake()
        {
            resourceGenerateData = GetComponent<BuildingTypeHolder>().buildingType.resourceGenerateData;
            timerMax = resourceGenerateData.timerMax;
            timer = timerMax;
        }

        private void Start()
        {

            int nearbyResourceAmount = GetNearbyResourceamount(resourceGenerateData, transform.position);
            if (nearbyResourceAmount == 0)
            {
                enabled = false;
            }
            else
            {
                timerMax = resourceGenerateData.timerMax / 2 +
                    resourceGenerateData.timerMax *
                    (1 - nearbyResourceAmount / resourceGenerateData.maxResourceAmount);
            }
            print("nearyByResourceAmount" + nearbyResourceAmount);
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = timerMax;
                //添加资源
                ResourceManager.Instance.AddResource(resourceGenerateData.resourceType, 1);
            }
        }

        public ResourceGenerateData GetResourceGeneratorData()
        {
            return resourceGenerateData;
        }

        public float GetTimerNormalized()
        {
            return timer / timerMax;
        }

        public float GetAmountGeneratedPerSecond()
        {
            return 1 / timerMax;
        }
    }
}
