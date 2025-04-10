using System;
using RPG_System.Modules.StateMachine;
using RPG_System.Modules.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace RPG_System.Modules.Entities.Runtime.Enemy
{
    public class EnemyStateMachine : FiniteStateMachine
    {
        [field: SerializeField] public Animator EnemyAnimator { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public float PlayerChasingRange { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        public GameObject CurrentTarget { get; private set; }

        private void Awake()
        {
            EnemyAnimator ??= GetComponentInChildren<Animator>();
            CurrentTarget ??= GameObject.FindWithTag("Player");
            Agent.updatePosition = false;
            Agent.updateRotation = false;
        }

        private void Start()
        {
            SwitchState(new EnemyIdleState(this));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        }
    }
}