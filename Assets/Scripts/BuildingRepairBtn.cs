using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class BuildingRepairBtn : MonoBehaviour
    {
        HealthSystem healthSystem;
        [SerializeField] ResourceTypeSO resourceType;
        private void Awake()
        {
            healthSystem = transform.root.GetComponent<HealthSystem>();

            transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                int healthNeedHeal = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
                int repairCost = healthNeedHeal / 2;
                ResourceAmount[] resourceCost = new ResourceAmount[]
                   { new ResourceAmount { resourceType = resourceType, amount = repairCost } };

                if (ResourceManager.Instance.CanAfford(resourceCost))
                {
                    ResourceManager.Instance.SpendResource(resourceCost);
                    healthSystem.HealFull();
                }
                else
                {
                    TooltipUI.Instance.Show("无法负担 " + repairCost + "G", new TooltipUI.TooltipTimer { timer = 2f });
                }

            });
        }

    }




}
