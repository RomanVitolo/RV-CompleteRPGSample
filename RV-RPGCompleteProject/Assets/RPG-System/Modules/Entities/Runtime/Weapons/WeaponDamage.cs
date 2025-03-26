using System;
using System.Collections.Generic;
using RPG_System.Modules.Entities.Runtime.Health;
using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Weapons
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        private int damageValue;
        
        private readonly List<Collider> alreadyCollideWith = new List<Collider>();

        private void OnEnable()
        {
            alreadyCollideWith.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other == _collider) return;
            
            if(alreadyCollideWith.Contains(other)) return;
            
            alreadyCollideWith.Add(other);

            if (other.TryGetComponent<EntityHealth>(out var health))
            {
                health.DealDamage(damageValue);
            }
        }

        public void SetAttack(int damage)
        {
            damageValue = damage;
        }
    }
}