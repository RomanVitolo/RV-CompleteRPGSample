using System;
using UnityEngine;

namespace RPG_System.Modules.Utilities
{
    public class ForceReceiver : MonoBehaviour
    {
        public Vector3 Movement => impactForce + Vector3.up * verticalVelocity;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float drag;
        
        private Vector3 impactForce;
        private Vector3 dampingVelocity;
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

            impactForce = Vector3.SmoothDamp(impactForce, Vector3.zero, ref dampingVelocity, drag);
        }

        public void AddForce(Vector3 force)
        {
            impactForce += force;
        }
    }
}