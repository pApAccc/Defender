using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    [CreateAssetMenu(fileName = "New BuildingTypeList", menuName = "ScriptableObject/New BuildingTypeList")]
    public class BuildingTypeListSO : ScriptableObject
    {
        public List<BuildingTypeSO> list;
    }
}
