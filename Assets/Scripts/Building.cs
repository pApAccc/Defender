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
        private BuildingRepairBtn buildingRepairBtn;
        private void Awake()
        {
            buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn") != null ?
                                  transform.Find("pfBuildingDemolishBtn").GetComponent<BuildingDemolishBtn>() : null;
            buildingRepairBtn = transform.Find("pfBuildingRepairBtn") != null ?
                                  transform.Find("pfBuildingRepairBtn").GetComponent<BuildingRepairBtn>() : null;

            HideDemolishBtn();
            HideRepairBtn();

        }
        private void Start()
        {
            buildingType = GetComponent<BuildingTypeHolder>().buildingType;
            healthSystem = GetComponent<HealthSystem>();

            healthSystem.OnDead += HealthSystem_OnDied;
            healthSystem.OnDamaged += Building_OnDamaged;
            healthSystem.OnHealed += Building_OnHealed;

            healthSystem.SetHealthAmountMax(buildingType.health, true);
        }

        private void Building_OnHealed(object sender, System.EventArgs e)
        {
            if (healthSystem.IsFullHealth())
            {
                HideRepairBtn();
            }
        }

        private void Building_OnDamaged(object sender, System.EventArgs e)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
            ShowRepairBtn();
        }

        private void HealthSystem_OnDied(object sender, System.EventArgs e)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
            Destroy(gameObject);
        }

        private void OnMouseEnter()
        {
            ShowDemolishBtn();
        }
        private void OnMouseExit()
        {
            HideDemolishBtn();
        }

        private void ShowDemolishBtn()
        {
            if (buildingDemolishBtn != null)
                buildingDemolishBtn.gameObject.SetActive(true);
        }

        private void HideDemolishBtn()
        {
            if (buildingDemolishBtn != null)
                buildingDemolishBtn.gameObject.SetActive(false);
        }

        private void ShowRepairBtn()
        {
            if (buildingRepairBtn != null)
                buildingRepairBtn.gameObject.SetActive(true);
        }

        private void HideRepairBtn()
        {
            if (buildingRepairBtn != null)
                buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
