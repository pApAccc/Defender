using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑属性类
/// </summary>
namespace ns
{
    [CreateAssetMenu(fileName = "New BuildingType", menuName = "ScriptableObject/New BuildingType")]
    public class BuildingTypeSO : ScriptableObject
    {
        public string nameString;
        public Transform Prefab;
        public Sprite sprite;
        public float minConsrructionRadius;
        public bool hasResourceGenerateData;
        public ResourceGenerateData resourceGenerateData;
        public ResourceAmount[] constructionResourceCostArray;
        public int health;
        public string GetConstuctionResourceCostString()
        {
            string str = "";

            foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
            {
                str += "<color=#" + resourceAmount.resourceType.colorHex + ">" +
                    resourceAmount.resourceType.nameShort + ":" + resourceAmount.amount + "</color> ";
            }

            return str;
        }
    }
}
