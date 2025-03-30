using System;
using RPG_System.Modules.StateMachine;
using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Enemy
{
    public class EnemyStateMachine : FiniteStateMachine
    {
        [field: SerializeField] public Animator EnemyAnimator { get; private set; }

        private void Awake()
        {
            EnemyAnimator ??= GetComponentInChildren<Animator>();
            
            SwitchState(new EnemyIdleState(this));
        }
    }
}