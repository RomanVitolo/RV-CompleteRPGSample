using RPG_System.Modules.StateMachine;
using UnityEngine;

namespace RPG_System.Modules.Characters.Runtime.Player
{
    public abstract class PlayerBaseState : State
    {
       protected PlayerStateMachine stateMachine;

       public PlayerBaseState(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;
    }
}