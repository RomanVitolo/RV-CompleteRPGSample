using RPG_System.Modules.StateMachine;
using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Enemy
{
    public abstract class EnemyBaseState : State
    {
        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        protected void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }
        protected void Move(Vector3 movement, float deltaTime)
        {
            stateMachine.Controller.Move((movement + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected bool isInChaseRange()
        {
            float targetDistance = (stateMachine.CurrentTarget.transform.position -
                                    stateMachine.transform.position).sqrMagnitude;
            return targetDistance <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
        }
    }
}