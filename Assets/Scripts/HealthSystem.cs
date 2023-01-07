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
        public event EventHandler OnHealed;
        [SerializeField] int healthAmountMax;
        int healthAmount;
        private void Awake()
        {
            healthAmount = healthAmountMax;
        }
        public void Damage(int amount)
        {
            healthAmount -= amount;
            healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

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
        public int GetHealthAmountMax()
        {
            return healthAmountMax;
        }

        public float GetHealthAmountNormailzed()
        {
            return (float)healthAmount / healthAmountMax;
        }

        public bool IsFullHealth()
        {
            return healthAmount == healthAmountMax;
        }
        public void SetHealthAmountMax(int healthAmount, bool updateHealthAmount)
        {
            healthAmountMax = healthAmount;

            if (updateHealthAmount)
            {
                this.healthAmount = healthAmountMax;
            }
        }

        public void Heal(int amount)
        {
            healthAmount += amount;
            healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

            OnHealed?.Invoke(this, EventArgs.Empty);
        }

        public void HealFull()
        {
            healthAmount = healthAmountMax;
            OnHealed?.Invoke(this, EventArgs.Empty);
        }
    }
}
