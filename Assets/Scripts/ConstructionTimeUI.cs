using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ConstructionTimeUI : MonoBehaviour
    {
        [SerializeField] private BuildingConstruction buildingConstruction;
        private Image constructionProcessImage;
        private void Awake()
        {
            constructionProcessImage = transform.FindChildByName("Image").GetComponent<Image>();
        }

        private void Update()
        {
            constructionProcessImage.fillAmount = buildingConstruction.GetConstructionTimeNormalized();
        }
    }
}
