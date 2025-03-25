using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Player
{
    public class PlayerMovementState : PlayerBaseState
    {
        private readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");
        private readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");
        private const float dampTime = 0.1f;

        public PlayerMovementState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputReader.TargetEvent += OnTargetAction;
            
            stateMachine.PlayerAnimator.Play(FreeLookBlendTree);
        }
        
        public override void Execute(float deltaTime)
        {
            Vector3 movement = CalculateMovement();
            
            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

            if (stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.PlayerAnimator.SetFloat(FreeLookSpeed, 0, dampTime, Time.deltaTime);
                return;
            }
            
            stateMachine.PlayerAnimator.SetFloat(FreeLookSpeed, 1, dampTime, Time.deltaTime);
            LookMovementDirection(movement, deltaTime);
        }
        
        public override void Exit()
        {
            stateMachine.InputReader.TargetEvent -= OnTargetAction;
        }
        
        private void OnTargetAction()
        {
            if (!stateMachine.Targeter.ChooseTarget()) return;
            
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }


        private void LookMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(
                stateMachine.transform.rotation, Quaternion.LookRotation(movement),
                deltaTime * stateMachine.RotationSmoothValue);
        }

        private Vector3 CalculateMovement()
        {
            var forward = stateMachine.MainCameraTransform.forward;
            var right = stateMachine.MainCameraTransform.right;
            
            forward.y = 0;
            right.y = 0;
            
            forward.Normalize();
            right.Normalize();
            
            return forward * stateMachine.InputReader.MovementValue.y + 
                   right * stateMachine.InputReader.MovementValue.x;
        }
        
    }
}