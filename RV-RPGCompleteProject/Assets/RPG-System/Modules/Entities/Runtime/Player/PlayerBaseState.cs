using RPG_System.Modules.StateMachine;
using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Player
{
    public abstract class PlayerBaseState : State
    {
       protected PlayerStateMachine stateMachine;

       protected PlayerBaseState(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;

       protected void Move(Vector3 movement, float deltaTime)
       {
           stateMachine.Controller.Move((movement + stateMachine.ForceReceiver.Movement) * deltaTime);
       }

       protected void LookAtTarget()
       {
           if(stateMachine.Targeter.CurrentTarget == null) return;

           Vector3 lookPosition = stateMachine.Targeter.CurrentTarget.TargetTransform().position -
                                  stateMachine.transform.position;

           lookPosition.y = 0;
           stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
           
       }
    }
}