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
        [SerializeField] bool runOnce;
        SortingGroup sortingGroup;
        SpriteRenderer spriteRender;
        private void Awake()
        {
            if (!TryGetComponent(out sortingGroup))
                TryGetComponent(out spriteRender);
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
