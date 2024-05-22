using System;
using System.Collections.Generic;
using System.Linq;
using GD.Datas.ValueObjects;
using GD.Enums;
using GD.Keys;
using GD.Signals;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GD.Managers
{
    [RequireComponent(typeof(PlayerInput))]
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables

        //

        #endregion

        #region Private Variables

        private PlayerInput _playerInput;
        private const string PathOfData = "GunduzDev/Datas/UIPanelDatas";
        private List<UIPanelData> _datas = new List<UIPanelData>();
        
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _playerInput.onActionTriggered += OnActionTriggered;
            UISignal.OnGetData += OnGetData;
            UISignal.OnUIEventSubscription += OnUIEventSubscription;
        }

        private void UnsubscribeEvents()
        {
            _playerInput.onActionTriggered -= OnActionTriggered;
            UISignal.OnGetData -= OnGetData;
            UISignal.OnUIEventSubscription -= OnUIEventSubscription;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        private void Awake()
        {
            GetReferences();
        }
        
        void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
            _datas = GetDatas();
        }
        
        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            // Hangi tuşun basıldığını belirle
            string actionName = context.action.name;

            // Tuşa basıldığında gerçekleşecek işlemi belirle
            if (context.performed)
            {
                switch (actionName)
                {
                    case ActionNames.Game:
                        OnStart();
                        break;
                    case ActionNames.Inventory:
                        OnInventory();
                        break;
                    case ActionNames.Shop:
                        OnShop();
                        break;
                    case ActionNames.Capture:
                        OnCapture();
                        break;
                    case ActionNames.Pause:
                        OnPause();
                        break;
                    case ActionNames.Settings:
                        OnSettings();
                        break;
                    case ActionNames.Escape:
                        OnClose();
                        break;
                }
            }
        }

        private List<UIPanelData> GetDatas()
        {
            return _datas = new List<UIPanelData>(Resources.LoadAll<CD_UIPanel>(PathOfData)
                .Select(item => item.Data)
                .ToList());
        }
        
        private UIPanelData OnGetData(UIPanelTypes type)
        {
            return _datas.Find(x => x.type == type);
        }

        void GetMyDatas()
        {
            foreach (var data in _datas)
            {
                Debug.Log(data.panelName + " is a panel." + 
                          "\n" + "Prefab: " + data.panelPrefab + 
                          "\n" + "Type: " + data.type + 
                          "\n" + "Layer: " + data.panelLayer + 
                          "\n" + "EscapePanel: " + data.escapePanel + 
                          "\n" + "OpenablePanels: " + data.openablePanels + 
                          "\n" + "Disableable: " + data.isDisableable);
            }
        }

        #region UI Events

        void OnUIEventSubscription(UIEventSubscriptionTypes types)
        {
            switch (types)
            {
                case UIEventSubscriptionTypes.OnDefault:
                    OnDefault();
                    break;
                case UIEventSubscriptionTypes.OnMenu:
                    OnMenu();
                    break;
                case UIEventSubscriptionTypes.OnStart:
                    OnStart();
                    break;
                case UIEventSubscriptionTypes.OnClose:
                    OnClose();
                    break;
                case UIEventSubscriptionTypes.OnQuit:
                    OnQuit();
                    break;
                case UIEventSubscriptionTypes.OnSettings:
                    OnSettings();
                    break;
                case UIEventSubscriptionTypes.OnConfirm:
                    OnConfirm();
                    break;
                case UIEventSubscriptionTypes.OnPause:
                    OnPause();
                    break;
                case UIEventSubscriptionTypes.OnShop:
                    OnShop();
                    break;
                case UIEventSubscriptionTypes.OnInventory:
                    OnInventory();
                    break;
                case UIEventSubscriptionTypes.OnCapture:
                    OnCapture();
                    break;
            }
        }
        
        void OnDefault()
        {
            
        }
        
        void OnMenu()
        {
            UISignal.OnCloseAllPanels?.Invoke();
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Menu));
        }
        
        void OnStart()
        {
            UISignal.OnCloseAllPanels?.Invoke();
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Game));
        }
        
        void OnClose()
        {
            UISignal.OnEscapeKeyPressed?.Invoke();
        }
        
        void OnQuit()
        {
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Quit));
        }
        
        void OnSettings()
        {
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Settings));
        }
        
        void OnConfirm()
        {
            
        }
        
        void OnPause()
        {
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Pause));
        }

        void OnInventory()
        {
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Inventory));
        }

        void OnShop()
        {
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Shop));
        }
        
        void OnCapture()
        {
            UISignal.OnOpenPanel(OnGetData(UIPanelTypes.Capture));
        }

        #endregion
    }
}