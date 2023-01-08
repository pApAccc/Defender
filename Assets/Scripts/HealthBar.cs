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
        Transform separatorContainer;
        Transform separatorTemplate;
        private void Awake()
        {
            healthBarTF = transform.Find("Bar");
        }
        private void Start()
        {
            separatorContainer = transform.Find("SeparatorContainer");
            separatorTemplate = separatorContainer.Find("SeparatorTemplate");
            ConstructHealthBarSeparator();

            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            healthSystem.OnHealed += HealthSystem_OnHealed;
            healthSystem.OnHealthMaxChanged += HealthSystem_OnHealthMaxChanged;

            UpdateHealthBar();
            UpdateHealthBarVisable();
        }

        private void ConstructHealthBarSeparator()
        {
            separatorTemplate.gameObject.SetActive(false);

            foreach (Transform separator in separatorContainer)
            {
                if (separator == separatorTemplate) continue;
                Destroy(separator.gameObject);
            }

            float barsize = 3f;
            float healthAmountPerSeparator = 10;

            float barOneHealthSize = barsize / healthSystem.GetHealthAmountMax();
            int separatorAmountCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

            for (int i = 0; i < separatorAmountCount; i++)
            {
                Transform separatorTF = Instantiate(separatorTemplate, separatorContainer);
                separatorTF.gameObject.SetActive(true);
                separatorTF.localPosition = new Vector3(barOneHealthSize * i * healthAmountPerSeparator, 0, 0);
            }
        }

        private void HealthSystem_OnHealthMaxChanged(object sender, System.EventArgs e)
        {
            ConstructHealthBarSeparator();
        }

        private void HealthSystem_OnHealed(object sender, System.EventArgs e)
        {
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
