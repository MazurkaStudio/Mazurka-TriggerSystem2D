using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheMazurkaStudio.TriggerSystem2D
{ 
    [AddComponentMenu("The Mazurka Studio/Trigger/Conditions/Trigger Tag Condition")]
    public class TriggerTagCondition : TriggerAdditionalCondition
    {
        [SerializeField, BoxGroup("Tags"), TagField] private string _excludeTag;
        [SerializeField, BoxGroup("Tags"), TagField] private string _requireTag;
        
        protected override bool EnterCondition(Collider2D other)
        {
            if(!string.IsNullOrEmpty(_excludeTag) && other.CompareTag(_excludeTag)) return false;
            if(!string.IsNullOrEmpty(_requireTag) && !other.CompareTag(_requireTag)) return false;
            return true;
        }

        protected override bool ExitCondition(Collider2D other)
        {
            if(!string.IsNullOrEmpty(_excludeTag) && other.CompareTag(_excludeTag)) return false;
            if(!string.IsNullOrEmpty(_requireTag) && !other.CompareTag(_requireTag)) return false;
            return true;
        }
    }
}
