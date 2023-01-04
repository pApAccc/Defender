using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] HealthSystem healthSystem;
        Transform healthBarTF;
        private void Awake()
        {
            healthBarTF = transform.Find("Bar");
        }
        private void Start()
        {
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            UpdateHealthBar();
            UpdateHealthBarVisable();
        }

        private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
        {
            UpdateHealthBar();
            UpdateHealthBarVisable();
        }

        void UpdateHealthBar()
        {
            healthBarTF.localScale = new Vector3(healthSystem.GetHealthAmountNormailzed(), 1, 1);
        }

        void UpdateHealthBarVisable()
        {
            if (healthSystem.IsFullHealth())
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
