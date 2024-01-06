using UnityEngine;

namespace TheMazurkaStudio.TriggerSystem2D
{
    public class TriggerActionDebug : TriggerAction
    {
        [SerializeField] private string message;
        protected override void TriggerEnter(Collider2D other)
        {
            Debug.Log(message + " on enter trigger" + _trigger.gameObject.name);
        }

        protected override void TriggerExit(Collider2D other)
        {
            Debug.Log(message + " on exit trigger" + _trigger.gameObject.name);
        }
    }
}
