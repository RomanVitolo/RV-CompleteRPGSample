using System;
using UnityEngine;

namespace RPG_System.Modules.CoreCombat.Runtime.Targets
{
    public interface ITarget
    {
        public Transform TargetTransform();
        public event Action<ITarget> NotifyDestroyerTarget;
    }
}