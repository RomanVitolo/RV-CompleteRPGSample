using RPG_System.Modules.StateMachine;

namespace RPG_System.Modules.Entities.Runtime.Enemy
{
    public abstract class EnemyBaseState : State
    {
        protected EnemyStateMachine stateMachine;

        public EnemyBaseState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}