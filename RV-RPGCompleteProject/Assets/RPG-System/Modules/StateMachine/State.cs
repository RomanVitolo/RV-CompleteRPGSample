namespace RPG_System.Modules.StateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Execute(float deltaTime);
        public abstract void Exit();
    }
}