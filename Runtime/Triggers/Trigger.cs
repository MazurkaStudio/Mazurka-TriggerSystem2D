using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheMazurkaStudio.TriggerSystem2D
{
    [AddComponentMenu("The Mazurka Studio/Trigger/Trigger")]
    public class Trigger : MonoBehaviour
    {
        public event Action<Collider2D> TriggerWasEntered;
        public event Action<Collider2D> TriggerWasExited;
        
        [SerializeField, BoxGroup("Parameters")] protected bool _useOnce;
        [SerializeField, BoxGroup("Parameters"), Layer] protected int _forceLayer;
        [SerializeField, BoxGroup("Parameters")] protected bool _detectTriggers;

        [SerializeField, BoxGroup("Parameters")] protected bool _checkEnter = true;
        [SerializeField, BoxGroup("Parameters")] protected bool _checkExit;

        [SerializeField, FoldoutGroup("Events")] protected UnityEvent<Collider2D> onTriggerEnter;
        [SerializeField, FoldoutGroup("Events")] protected UnityEvent<Collider2D> onTriggerExit;

        protected bool _haveTrigger;

        private Collider2D _collider;

        public bool IsEnable
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        protected virtual void Awake()
        {
            _collider = GetComponentInChildren<CompositeCollider2D>();
            if (_collider == null)
            {
                _collider = GetComponentInChildren<Collider2D>();
                if (_collider == null) enabled = false;
            }
           
        }
        
        private void OnValidate()
        {
//            gameObject.layer = _forceLayer;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!_checkEnter) return;
            if (_haveTrigger && _useOnce) return;
            if (other.isTrigger && !_detectTriggers) return;
            
           
            
            if (_allConditions.Any(condition => !condition.TriggerEnterCondition(other))) return;
      
            TriggerEnter(other);
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!_checkExit) return;
            if (_haveTrigger && _useOnce) return;
            if (other.isTrigger && !_detectTriggers) return;


            if (_allConditions.Any(condition => !condition.TriggerExitCondition(other))) return;

            TriggerExit(other);
        }
        
        
        protected virtual void TriggerEnter(Collider2D other)
        {
            _haveTrigger = true;
            onTriggerEnter?.Invoke(other);
            TriggerWasEntered?.Invoke(other);
        }

        protected virtual void TriggerExit(Collider2D other)
        {
            _haveTrigger = true;
            onTriggerExit?.Invoke(other);
            TriggerWasExited?.Invoke(other);
        }

        
        private readonly HashSet<TriggerAdditionalCondition> _allConditions = new();

        public void AddTriggerCondition(TriggerAdditionalCondition condition)
        { 
            if(!_allConditions.Contains(condition)) _allConditions.Add(condition);
        }
        
        public void RemoveTriggerCondition(TriggerAdditionalCondition condition)
        {
            if(_allConditions.Contains(condition)) _allConditions.Remove(condition);
        }
    }
}
