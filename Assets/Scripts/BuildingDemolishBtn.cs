using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuildingDemolishBtn : MonoBehaviour
    {
        Building building;
        private void Awake()
        {
            building = transform.root.GetComponent<Building>();


            transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
                foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
                {
                    ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * .6f));
                }

                Destroy(building.gameObject);

            });
        }

    }




}
