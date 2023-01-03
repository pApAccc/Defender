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
        HealthSystem healthSystem;
        BuildingTypeSO buildingType;
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
    }
}
