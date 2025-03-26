using RPG_System.Modules.CoreCombat.Runtime.Targets;
using RPG_System.Modules.Entities.Runtime.Weapons;
using RPG_System.Modules.InputSystem.Runtime;
using RPG_System.Modules.StateMachine;
using RPG_System.Modules.Utilities;
using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Player
{
    public class PlayerStateMachine : FiniteStateMachine
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator PlayerAnimator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
        [field: SerializeField] public WeaponDamage Weapon { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSmoothValue { get; private set; }
        [field: SerializeField] public AttackModel[] Attacks { get; private set; }
        
        public Transform MainCameraTransform { get; private set; }

        private void Awake()
        {
            InputReader.InitializeInputs();
            Controller ??= GetComponent<CharacterController>();
            PlayerAnimator ??= GetComponentInChildren<Animator>();
            Targeter ??= GetComponentInChildren<Targeter>();
            Weapon ??= GetComponentInChildren<WeaponDamage>();
            if (Camera.main != null) MainCameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            SwitchState(new PlayerMovementState(this));
        }

        private void OnDestroy()
        {
            InputReader.DeInitializeInputs();
        }
    }
}