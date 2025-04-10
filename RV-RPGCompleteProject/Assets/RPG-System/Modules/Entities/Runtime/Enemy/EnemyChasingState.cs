using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly int Locomotion = Animator.StringToHash("Locomotion");
        private readonly int Speed = Animator.StringToHash("Speed");
        private const float CROSSFADEDURATION = 0.1f;
        private const float DAMPTIME = 0.1f;
        
        public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Execute(float deltaTime)
        {
            if (!isInChaseRange())
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                return;
            }

            MoveToTarget(deltaTime);
            
            stateMachine.EnemyAnimator.SetFloat(Speed, 1f, DAMPTIME, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.Agent.ResetPath();
            stateMachine.Agent.velocity = Vector3.zero;
        }

        private void MoveToTarget(float deltaTime)
        {
            stateMachine.Agent.destination = stateMachine.CurrentTarget.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);

            stateMachine.Agent.velocity = stateMachine.Controller.velocity;
        }
    }
}