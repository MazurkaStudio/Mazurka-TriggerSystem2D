using UnityEngine;

namespace TheMazurkaStudio.TriggerSystem2D
{
    /// <summary>
    ///  Trigger action execute actions on trigger events when is child of the trigger gameObject
    /// </summary>
    public abstract class TriggerAction : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField, TextArea] private string _comments;
        #endif

        protected Trigger _trigger;

        private void OnEnable()
        {
            _trigger = GetComponentInParent<Trigger>();

            if (_trigger == null)
            {
                enabled = false;
                return;
            }

            _trigger.TriggerWasEntered += TriggerEnter;
            _trigger.TriggerWasExited += TriggerExit;
        }

        private void OnDisable()
        {
            if (_trigger == null) return;
            
            _trigger.TriggerWasEntered -= TriggerEnter;
            _trigger.TriggerWasExited -= TriggerExit;
        }

        protected abstract void TriggerEnter(Collider2D other);

        protected abstract void TriggerExit(Collider2D other);
    }
}
