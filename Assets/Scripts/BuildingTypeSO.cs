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
        public ResourceGenerateData resourceGenerateData;
    }
}
