using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class SpriteSorting : MonoBehaviour
    {
        [SerializeField] bool runOnce = true;
        SortingGroup sortingGroup;
        SpriteRenderer spriteRender;
        private void Awake()
        {
            sortingGroup = GetComponentInChildren<SortingGroup>();
            spriteRender = GetComponentInChildren<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            float precisionMult = 5f;
            if (sortingGroup != null)
                sortingGroup.sortingOrder = (int)(-transform.position.y * precisionMult);
            else
            {
                spriteRender.sortingOrder = (int)(-transform.position.y * precisionMult);
            }
            if (runOnce)
            {
                Destroy(this);
            }
        }
    }
}
