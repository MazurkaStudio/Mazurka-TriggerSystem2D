using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheMazurkaStudio.TriggerSystem2D
{ 
    [AddComponentMenu("The Mazurka Studio/Trigger/Delay Trigger")]
    public class DelayTrigger : Trigger
    {
        [SerializeField, BoxGroup("Parameters")] private float _enterDelay =1f;
        [SerializeField, BoxGroup("Parameters")] private float _exitDelay = 1f;
        [Tooltip("Should enter before for trigger exit, and exit before to trigger enter")]
        [SerializeField, BoxGroup("Parameters")] private bool _toggleBehaviour = true;


        private HashSet<Collider2D> _insideColliders;
        private Dictionary<Collider2D, Coroutine> _enterDelayAction;
        private Dictionary<Collider2D, Coroutine> _exitDelayAction;
        
        protected override void Awake()
        {
            base.Awake();
            _insideColliders = new HashSet<Collider2D>();
            _enterDelayAction = new Dictionary<Collider2D, Coroutine>();
            _exitDelayAction = new Dictionary<Collider2D, Coroutine>();
        }
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (_exitDelayAction.TryGetValue(other, out var coroutine))
            {
                StopCoroutine(coroutine);
                _exitDelayAction.Remove(other);
            }
            
            if (_insideColliders.Contains(other))
            {
                if (_toggleBehaviour) return;
            }
            else if (!_checkEnter) _insideColliders.Add(other);
            
            base.OnTriggerEnter2D(other);
        }
        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (_enterDelayAction.TryGetValue(other, out var coroutine))
            {
                StopCoroutine(coroutine);
                _enterDelayAction.Remove(other);
            }

            if (!_insideColliders.Contains(other))
            {
                if (_toggleBehaviour) return;
            }
            else if (!_checkExit) _insideColliders.Remove(other);

            base.OnTriggerExit2D(other);
        }
        
        
        protected override void TriggerEnter(Collider2D other)
        {
            
            
            if (_enterDelay > 0f)
            {
                var enterCoroutine = StartCoroutine(DelayEnterTrigger(_enterDelay, other));
                _enterDelayAction.Add(other, enterCoroutine);
                return;
            }
           
            base.TriggerEnter(other);
        }
        protected override void TriggerExit(Collider2D other)
        {
          
            
            if (_exitDelay > 0f)
            {
                var exitCoroutine = StartCoroutine(DelayExitTrigger(_exitDelay, other));
                _exitDelayAction.Add(other, exitCoroutine);
                return;
            }
            
            base.TriggerExit(other);
        }

        
        private IEnumerator DelayEnterTrigger(float delay, Collider2D other)
        {
            yield return new WaitForSeconds(delay);
            base.TriggerEnter(other);
            _enterDelayAction.Remove(other);
            _insideColliders.Add(other);
        }
        private IEnumerator DelayExitTrigger(float delay, Collider2D other)
        {
            yield return new WaitForSeconds(delay);
            base.TriggerExit(other);
            _exitDelayAction.Remove(other);
            _insideColliders.Remove(other);
        }
    }
}
