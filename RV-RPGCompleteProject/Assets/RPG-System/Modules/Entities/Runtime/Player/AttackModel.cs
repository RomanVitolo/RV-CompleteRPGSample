using UnityEngine;

namespace RPG_System.Modules.Entities.Runtime.Player
{
    [CreateAssetMenu(menuName = "RPG System Modules/Entities/Player/AttackModel")]
    public class AttackModel : ScriptableObject
    {
        [field: SerializeField] public string AnimationName { get; private set; }   
        [field: SerializeField] public float TransitionDuration { get; private set; }
        [field: SerializeField] public int CombatStateIndex { get; private set; } = -1;
        [field: SerializeField] public float CombatAttackTime { get; private set; }
        [field: SerializeField] public float ForceTime { get; private set; }
        [field: SerializeField] public float Force { get; private set; }
        [field: SerializeField] public int DamageValue { get; private set; }
    }
}