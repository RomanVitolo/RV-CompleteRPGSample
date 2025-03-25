using System;
using UnityEngine;

namespace RPG_System.Modules.CoreCombat.Runtime.Targets
{
    public class Target : MonoBehaviour, ITarget
    {
        public Transform TargetTransform() => transform;
        public event Action<ITarget> NotifyDestroyerTarget;


        private void OnDestroy()
        {
            NotifyDestroyerTarget?.Invoke(this);
        }
    }
}