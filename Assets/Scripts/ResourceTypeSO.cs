using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    [CreateAssetMenu(fileName = "New ResourceType", menuName = "ScriptableObject/New ResourceType")]
    public class ResourceTypeSO : ScriptableObject
    {
        public string nameString;
        public string nameShort;
        public Sprite sprite;
        public string colorHex;

    }
}
