using System;
using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Health
{
    
    public class EntityHealth : MonoBehaviour
    {
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public int CurrentHealth { get; set; }

        public void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public void DealDamage(int damage)
        {
            if (CurrentHealth <= 0) return;
            
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            
            Debug.Log(CurrentHealth);
        }
    }
}