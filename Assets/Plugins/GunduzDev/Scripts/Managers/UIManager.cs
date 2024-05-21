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
        private List<UIPanelData> _uiPanelDatas = new List<UIPanelData>();
        
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _playerInput.onActionTriggered += OnActionTriggered;
            UISignal.OnGetUIPanelData += OnGetUIPanelData;
            UISignal.OnUIEventSubscription += OnUIEventSubscription;
        }

        private void UnsubscribeEvents()
        {
            _playerInput.onActionTriggered -= OnActionTriggered;
            UISignal.OnGetUIPanelData -= OnGetUIPanelData;
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
            _uiPanelDatas = GetUIPanelDatas();
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
                        OnEscapeKeyPressed();
                        break;
                }
            }
        }

        private List<UIPanelData> GetUIPanelDatas()
        {
            return _uiPanelDatas = new List<UIPanelData>(Resources.LoadAll<CD_UIPanel>(PathOfData)
                .Select(item => item.Data)
                .ToList());
        }
        
        private UIPanelData OnGetUIPanelData(UIPanelTypes type)
        {
            return _uiPanelDatas.Find(x => x.panelType == type);
        }

        void GetMyUIPanelDatas()
        {
            foreach (var uiPanel in _uiPanelDatas)
            {
                Debug.Log(uiPanel.panelName + " is a panel." + 
                          "\n" + "Prefab: " + uiPanel.panelPrefab + 
                          "\n" + "Type: " + uiPanel.panelType + 
                          "\n" + "Layer: " + uiPanel.panelLayer + 
                          "\n" + "EscapePanel: " + uiPanel.escapePanel + 
                          "\n" + "OpenablePanels: " + uiPanel.openablePanels + 
                          "\n" + "Disableable: " + uiPanel.isDisableable);
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
                case UIEventSubscriptionTypes.OnStart:
                    OnStart();
                    break;
                case UIEventSubscriptionTypes.OnMenu:
                    OnMenu();
                    break;
                case UIEventSubscriptionTypes.OnConfirm:
                    OnConfirm();
                    break;
                case UIEventSubscriptionTypes.OnInventory:
                    OnInventory();
                    break;
                case UIEventSubscriptionTypes.OnShop:
                    OnShop();
                    break;
                case UIEventSubscriptionTypes.OnCapture:
                    OnCapture();
                    break;
                case UIEventSubscriptionTypes.OnPause:
                    OnPause();
                    break;
                case UIEventSubscriptionTypes.OnSettings:
                    OnSettings();
                    break;
                case UIEventSubscriptionTypes.OnClose:
                    OnEscapeKeyPressed();
                    break;
                case UIEventSubscriptionTypes.OnQuit:
                    OnQuit();
                    break;
            
            }
        }
        
        public void OnDefault()
        {
            
        }
        
        public void OnStart()
        {
            UISignal.OnCloseAllPanels?.Invoke();
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Game));
        }
        
        public void OnMenu()
        {
            UISignal.OnCloseAllPanels?.Invoke();
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Menu));
        }

        public void OnConfirm()
        {
            
        }
        
        public void OnInventory()
        {
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Inventory));
        }

        public void OnShop()
        {
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Shop));
        }
        
        public void OnCapture()
        {
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Capture));
        }
        
        public void OnPause()
        {
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Pause));
        }

        public void OnSettings()
        {
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Settings));
        }
        
        public void OnQuit()
        {
            UISignal.OnOpenPanel(OnGetUIPanelData(UIPanelTypes.Quit));
        }
        
        public void OnEscapeKeyPressed()
        {
            UISignal.OnEscapeKeyPressed?.Invoke();
        }

        #endregion
    }
}