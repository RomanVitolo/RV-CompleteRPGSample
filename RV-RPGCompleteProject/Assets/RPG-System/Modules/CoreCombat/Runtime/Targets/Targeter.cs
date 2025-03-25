using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace RPG_System.Modules.CoreCombat.Runtime.Targets
{
    public class Targeter : MonoBehaviour
    {
        public ITarget CurrentTarget { get; private set; }
        
        [SerializeField] private CinemachineTargetGroup _cineTargetGroup;
        private Camera camera;
        
        private readonly List<ITarget> targets = new List<ITarget>();

        private void Start()
        {
            camera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ITarget>(out var target)) return;
                
            targets.Add(target);
            target.NotifyDestroyerTarget += RemoveTarget;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<ITarget>(out var target)) return;
                
            RemoveTarget(target);
        }

        public bool ChooseTarget()
        {
            if (targets.Count == 0) return false;

            ITarget closestTarget = null;
            float closestTargetDistance = Mathf.Infinity;
            
            foreach (ITarget target in targets)
            {
                Vector2 viewPosition = camera.WorldToViewportPoint(target.TargetTransform().position);

                if (viewPosition.x < 0 || viewPosition.x > 1 || viewPosition.y < 0 || viewPosition.y > 1) 
                    continue;
                
                Vector3 toCenter = viewPosition - new Vector2(0.5f, 0.5f);
                if (toCenter.sqrMagnitude < closestTargetDistance)
                {
                    closestTarget = target;
                    closestTargetDistance = toCenter.sqrMagnitude;
                }
            }
            
            if(closestTarget == null) return false;

            CurrentTarget = closestTarget;
            _cineTargetGroup.AddMember(CurrentTarget.TargetTransform(), 1f, 2f);
            
            return true;
        }

        public void Cancel()
        {
            if(CurrentTarget == null) return;

            RemoveTargetsFromCineGroup();
        }

        private void RemoveTarget(ITarget target)
        {
            if (CurrentTarget == target)
            {
                RemoveTargetsFromCineGroup();
            }

            target.NotifyDestroyerTarget -= RemoveTarget;
            targets.Remove(target);
        }
        
        private void RemoveTargetsFromCineGroup()
        {
            _cineTargetGroup.RemoveMember(CurrentTarget.TargetTransform());
            CurrentTarget = null;
        }
    }
}