using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    [CreateAssetMenu(fileName = "New ResourceTypeList", menuName = "ScriptableObject/New ResourceTypeList")]
    public class ResourceTypeListSO : ScriptableObject
    {
        public List<ResourceTypeSO> list;
    }
}
