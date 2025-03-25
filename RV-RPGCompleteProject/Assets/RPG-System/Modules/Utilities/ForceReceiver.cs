using System;
using UnityEngine;

namespace RPG_System.Modules.Utilities
{
    public class ForceReceiver : MonoBehaviour
    {
        public Vector3 Movement => Vector3.up * verticalVelocity;
        
        [SerializeField] private CharacterController _characterController;
        
        private float verticalVelocity;

        private void Awake()
        {
            _characterController ??= GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (verticalVelocity < 0f && _characterController.isGrounded)
            {
                verticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }
    }
}