using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class Building : MonoBehaviour
    {
        private HealthSystem healthSystem;
        private BuildingTypeSO buildingType;
        private BuildingDemolishBtn buildingDemolishBtn;
        private void Awake()
        {
            buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn") != null ?
                                  transform.Find("pfBuildingDemolishBtn").GetComponent<BuildingDemolishBtn>() : null;

            if (buildingDemolishBtn != null)
                buildingDemolishBtn.gameObject.SetActive(false);
        }
        private void Start()
        {
            buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            healthSystem = GetComponent<HealthSystem>();
            healthSystem.OnDead += HealthSystem_OnDied;
            healthSystem.SetHealthAmountMax(buildingType.health, true);

        }
        private void HealthSystem_OnDied(object sender, System.EventArgs e)
        {
            Destroy(gameObject);
        }

        private void OnMouseEnter()
        {
            if (buildingDemolishBtn != null)
                buildingDemolishBtn.gameObject.SetActive(true);
        }

        private void OnMouseExit()
        {
            if (buildingDemolishBtn != null)
                buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
}
