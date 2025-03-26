using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private readonly AttackModel _attackStats;
        private float previousFrameTime;
        private bool alreadyForceApplied;
        
        public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
        {
            _attackStats = stateMachine.Attacks[attackIndex];
        }

        public override void Enter()
        {
            stateMachine.Weapon.SetAttack(_attackStats.DamageValue);
            stateMachine.PlayerAnimator.CrossFadeInFixedTime(_attackStats.AnimationName, _attackStats.TransitionDuration);
        }

        public override void Execute(float deltaTime)
        {
            Move(deltaTime);
            
            LookAtTarget();
            
            float normalizeTime = GetNormalizedTime();

            if (normalizeTime < 1f)
            {
                if (normalizeTime > previousFrameTime && normalizeTime < 1f)
                    TryApplyForce();
                
                if (stateMachine.InputReader.IsAttacking)
                    TryCombatAttack(normalizeTime);
            }
            else
            {
                if (stateMachine.Targeter.CurrentTarget != null)
                    stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                
                else
                    stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            }
            
            previousFrameTime = normalizeTime;
        }

        public override void Exit()
        {
            
        }

        private float GetNormalizedTime()
        {
           AnimatorStateInfo currentInfo = stateMachine.PlayerAnimator.GetCurrentAnimatorStateInfo(0);
           AnimatorStateInfo nextInfo = stateMachine.PlayerAnimator.GetCurrentAnimatorStateInfo(0);

           if (stateMachine.PlayerAnimator.IsInTransition(0) && nextInfo.IsTag("Attack"))
           {
               return nextInfo.normalizedTime;
           }
           else if (!stateMachine.PlayerAnimator.IsInTransition(0) && currentInfo.IsTag("Attack"))
           {
               return currentInfo.normalizedTime;
           }
           else
           {
               return 0f;
           }
        }

        private void TryCombatAttack(float normalizedTime)
        {
            if(_attackStats.CombatStateIndex == -1) return;
            
            if(normalizedTime < _attackStats.CombatAttackTime) return;
            
            stateMachine.SwitchState
            (
                new PlayerAttackingState(stateMachine, _attackStats.CombatStateIndex)
            );
        }

        private void TryApplyForce()
        {
            if (alreadyForceApplied) return;
            
            stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * _attackStats.Force);
            
            alreadyForceApplied = true;
        }
    }
}