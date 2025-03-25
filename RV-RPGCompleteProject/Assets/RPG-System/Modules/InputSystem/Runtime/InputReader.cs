using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG_System.Modules.InputSystem.Runtime
{
    [CreateAssetMenu(menuName = "RPG System Module/Inputs", fileName = "InputReader")]
    public class InputReader : ScriptableObject, GlobalInputs.IPlayerActions
    {
        public event Action JumpEvent;
        public event Action DodgeEvent;
        public event Action TargetEvent;
        public event Action CancelEvent;
        public Vector2 MovementValue {get; private set;}
        public Vector2 LookValue {get; private set;}
        
        
        private GlobalInputs controls;

        public void InitializeInputs()
        {
            controls = new GlobalInputs();
            controls.Player.SetCallbacks(this);
            
            controls.Player.Enable();
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
                JumpEvent?.Invoke();
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            DodgeEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            
        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            
            TargetEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            
            CancelEvent?.Invoke();
        }

        public void DeInitializeInputs()
        {
            controls.Player.Disable();
        }
    }
}