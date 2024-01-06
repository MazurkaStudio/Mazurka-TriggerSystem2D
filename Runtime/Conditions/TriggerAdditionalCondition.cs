using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheMazurkaStudio.TriggerSystem2D
{
    public abstract class TriggerAdditionalCondition : MonoBehaviour
    {
        [SerializeField, BoxGroup("Parameters")]  private bool _useOnEnter = true;
        [SerializeField, BoxGroup("Parameters")]  private bool _useOnExit = true;

        protected Trigger _trigger;
        
        protected virtual void OnEnable()
        {
            _trigger = GetComponentInParent<Trigger>();

            if (_trigger == null) 
            {
                enabled = false;
                return;
            }
            
            _trigger.AddTriggerCondition(this);
        }

        protected virtual void OnDisable()
        {
            if (_trigger == null) return;
            
            _trigger.RemoveTriggerCondition(this);
        }

    

        public bool TriggerEnterCondition(Collider2D other)
        {
            return !_useOnEnter || EnterCondition(other);
        }
        
        public bool TriggerExitCondition(Collider2D other)
        {
            return !_useOnExit || ExitCondition(other);
        }

        protected abstract bool EnterCondition(Collider2D other);
        protected abstract bool ExitCondition(Collider2D other);
    }
    
}
