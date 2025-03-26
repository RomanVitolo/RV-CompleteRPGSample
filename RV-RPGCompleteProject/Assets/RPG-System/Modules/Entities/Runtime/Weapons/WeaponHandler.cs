using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponBehaviour;

        public void EnableWeapon()
        {
            _weaponBehaviour.SetActive(true);
        }
        
        public void DisableWeapon()
        {
            _weaponBehaviour.SetActive(false);
        }
    }
}