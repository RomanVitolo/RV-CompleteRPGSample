using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Enemy
{
    public class EnemyIdleState : EnemyBaseState
    {
        private readonly int Locomotion = Animator.StringToHash("Locomotion");
        private readonly int Speed = Animator.StringToHash("Speed");
        private const float CROSSFADEDURATION = 0.1f;
        private const float DAMPTIME = 0.1f;
        
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.EnemyAnimator.CrossFadeInFixedTime(Locomotion, CROSSFADEDURATION);
            
        }

        public override void Execute(float deltaTime)
        {
            stateMachine.EnemyAnimator.SetFloat(Speed, 0, DAMPTIME, deltaTime);
        }

        public override void Exit()
        {
           
        }
    }
}