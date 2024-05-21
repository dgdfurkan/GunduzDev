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

        private UIManager _uiManager;

        #endregion
        
        private void Awake()
        {
            FindReferences();
        }
        
        private void FindReferences()
        {
            _uiManager ??= FindObjectOfType<UIManager>();
            triggerEvent ??= GetComponent<EventTrigger>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            UISignal.OnUIEventSubscription?.Invoke(subscriptionType);
            
            // switch (subscriptionType)
            // {
            //     case UIEventSubscriptionTypes.OnDefault:
            //         print("OnDefault");
            //         _uiManager.OnDefault();
            //         break;
            //     case UIEventSubscriptionTypes.OnStart:
            //         print("OnStart");
            //         _uiManager.OnStart();
            //         break;
            //     case UIEventSubscriptionTypes.OnMenu:
            //         print("OnMenu");
            //         _uiManager.OnMenu();
            //         break;
            //     case UIEventSubscriptionTypes.OnConfirm:
            //         print("OnInventory");
            //         _uiManager.OnConfirm();
            //         break;
            //     case UIEventSubscriptionTypes.OnInventory:
            //         print("OnInventory");
            //         _uiManager.OnInventory();
            //         break;
            //     case UIEventSubscriptionTypes.OnShop:
            //         print("OnShop");
            //         _uiManager.OnShop();
            //         break;
            //     case UIEventSubscriptionTypes.OnCapture:
            //         print("OnCapture");
            //         _uiManager.OnCapture();
            //         break;
            //     case UIEventSubscriptionTypes.OnPause:
            //         print("OnPause");
            //         _uiManager.OnPause();
            //         break;
            //     case UIEventSubscriptionTypes.OnSettings:
            //         print("OnSettings");
            //         _uiManager.OnSettings();
            //         break;
            //     case UIEventSubscriptionTypes.OnClose:
            //         print("OnClose");
            //         _uiManager.OnEscapeKeyPressed();
            //         break;
            //     case UIEventSubscriptionTypes.OnQuit:
            //         print("OnQuit");
            //         _uiManager.OnQuit();
            //         break;
            // }
        }
    }
}

