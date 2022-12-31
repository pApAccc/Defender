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
        BuildingTypeSO buildingType;
        float timer;
        //过多久增加一次资源
        float timerMax;
        private void Awake()
        {
            buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            timerMax = buildingType.resourceGenerateData.timerMax;
            timer = timerMax;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = timerMax;
                //添加资源
                ResourceManager.Instance.AddResource(buildingType.resourceGenerateData.resourceType, 1);
            }
        }
    }
}
