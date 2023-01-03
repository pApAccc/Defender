using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class HealthSystem : MonoBehaviour
    {
        public event EventHandler OnDamaged;
        public event EventHandler OnDead;
        [SerializeField] int HealthAmountMax;
        int healthAmount;
        private void Awake()
        {
            healthAmount = HealthAmountMax;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Damage(10);
            }
        }
        public void Damage(int amount)
        {
            healthAmount -= amount;
            healthAmount = Mathf.Clamp(healthAmount, 0, HealthAmountMax);

            OnDamaged?.Invoke(this, EventArgs.Empty);
            if (IsDead())
            {
                OnDead?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsDead()
        {
            return healthAmount == 0;
        }

        public int GetHealthAmount()
        {
            return healthAmount;
        }

        public float GetHealthAmountNormailzed()
        {
            return (float)healthAmount / HealthAmountMax;
        }

        public bool IsFullHealth()
        {
            return healthAmount == HealthAmountMax;
        }
        public void SetHealthAmountMax(int healthAmount, bool updateHealthAmount)
        {
            HealthAmountMax = healthAmount;

            if (updateHealthAmount)
            {
                this.healthAmount = HealthAmountMax;
            }
        }
    }
}
