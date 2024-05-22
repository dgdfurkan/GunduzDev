using GD.Enums;
using GD.Managers;
using GD.Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GD.Handlers
{
    public class UIEventSubscriber : MonoBehaviour, IPointerClickHandler
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscriptionTypes subscriptionType;
        [SerializeField] private EventTrigger triggerEvent;

        #endregion
        
        #endregion
        
        private void Awake()
        {
            FindReferences();
        }
        
        private void FindReferences()
        {
            triggerEvent ??= GetComponent<EventTrigger>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            UISignal.OnUIEventSubscription?.Invoke(subscriptionType);
        }
    }
}

