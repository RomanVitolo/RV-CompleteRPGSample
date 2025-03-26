using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Player
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
        private readonly int TargetingForward = Animator.StringToHash("TargetingForwardSpeed");
        private readonly int TargetingRight = Animator.StringToHash("TargetingRightSpeed");
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancelAction;
            
            stateMachine.PlayerAnimator.CrossFadeInFixedTime(TargetingBlendTree, 0.1f);
        }

        public override void Execute(float deltaTime)
        {
            if ((stateMachine.InputReader.IsAttacking))
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerMovementState(stateMachine));
                return;
            }
            
            Vector3 movement = CalculateMovement();
            
            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

            UpdateAnimator(deltaTime);
            
            LookAtTarget();
        }

        public override void Exit()
        {
            stateMachine.InputReader.CancelEvent -= OnCancelAction;
        }

        private void OnCancelAction()
        {
            stateMachine.Targeter.Cancel();
            
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
        }

        private Vector3 CalculateMovement()
        {
            Vector3 movement = new Vector3();
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
            return movement;
        }

        private void UpdateAnimator(float deltaTime)
        {
            if (stateMachine.InputReader.MovementValue.y == 0)
            {
                stateMachine.PlayerAnimator.SetFloat(TargetingForward, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
                stateMachine.PlayerAnimator.SetFloat(TargetingForward, value, 0.1f, deltaTime);
            }
            
            if (stateMachine.InputReader.MovementValue.x == 0)
            {
                stateMachine.PlayerAnimator.SetFloat(TargetingRight, 0, 0.1f, deltaTime);
            }
            else
            {
                float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
                stateMachine.PlayerAnimator.SetFloat(TargetingRight, value, 0.1f, deltaTime);
            }
        }
    }
}