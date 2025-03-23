using UnityEngine;

namespace RPG_System.Modules.StateMachine
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        private State currentState;

        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
        
        private void Update()
        {
            currentState?.Execute(Time.deltaTime);
        }
    }
}