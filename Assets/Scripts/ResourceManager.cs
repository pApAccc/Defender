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

        private void Awake()
        {
            Instance = this;

            resourceAmountDict = new Dictionary<ResourceTypeSO, int>();
            ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            foreach (ResourceTypeSO res in resourceTypeListSO.list)
            {
                resourceAmountDict[res] = 0;
            }
            Test();
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
            Test();
        }

        public int GetReourceAmount(ResourceTypeSO resourceType)
        {
            return resourceAmountDict[resourceType];
        }
    }
}
