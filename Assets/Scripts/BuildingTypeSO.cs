using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    [CreateAssetMenu(fileName = "New BuildingType", menuName = "ScriptableObject/New BuildingType")]
    public class BuildingTypeSO : ScriptableObject
    {
        public string nameString;
        public Transform Prefab;
    }
}
