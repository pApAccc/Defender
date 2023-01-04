using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        public event EventHandler OnResourceAmountChanged;

        Dictionary<ResourceTypeSO, int> resourceAmountDict;
        [SerializeField] List<ResourceAmount> resourceAmountList;
        private void Awake()
        {
            Instance = this;

            resourceAmountDict = new Dictionary<ResourceTypeSO, int>();
            ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            foreach (ResourceTypeSO res in resourceTypeListSO.list)
            {
                resourceAmountDict[res] = 0;
            }

            foreach (ResourceAmount resourceAmount in resourceAmountList)
            {
                AddResource(resourceAmount.resourceType, resourceAmount.amount);
            }

        }

        void Test()
        {
            foreach (ResourceTypeSO res in resourceAmountDict.Keys)
            {
                print(res.name + ":" + resourceAmountDict[res]);
            }
        }

        public void AddResource(ResourceTypeSO resourceTypeSO, int amount)
        {
            resourceAmountDict[resourceTypeSO] += amount;
            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetReourceAmount(ResourceTypeSO resourceType)
        {
            return resourceAmountDict[resourceType];
        }

        /// <summary>
        /// 能负担资源开销吗
        /// </summary>
        /// <param name="resourceAmountArray"></param>
        /// <returns></returns>
        public bool CanAfford(ResourceAmount[] resourceAmountArray)
        {
            foreach (ResourceAmount resourceAmount in resourceAmountArray)
            {
                if (GetReourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
                {
                    //能买的起
                }
                else
                {
                    //买不起
                    return false;
                }
            }
            return true;
        }

        public void SpendResource(ResourceAmount[] resourceAmountArray)
        {
            foreach (ResourceAmount resourceAmount in resourceAmountArray)
            {
                resourceAmountDict[resourceAmount.resourceType] -= resourceAmount.amount;
            }
        }
    }
}
