using System;
using System.Collections.Generic;
using System.Linq;
using GD.Datas.ValueObjects;
using GD.Enums;
using GD.Signals;
using TMPro;
using UnityEngine;

namespace GD.Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> layers = new List<GameObject>();

        #endregion

        #region Private Variables

        //

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignal.OnOpenPanelByName += OnOpenPanelByName;
            UISignal.OnOpenPanel += OnOpenPanel;
            UISignal.OnClosePanelByType += OnClosePanelByType;
            UISignal.OnClosePanelByLayer += OnClosePanelByLayer;
            UISignal.OnCloseTopPanel = OnCloseTopPanel;
            UISignal.OnCloseAllPanels += OnCloseAllPanels;
            UISignal.OnSetPanelState += OnSetPanelState;
            UISignal.OnEscapeKeyPressed += OnEscapeKeyPressed;
        }

        private void UnsubscribeEvents()
        {
            UISignal.OnOpenPanelByName -= OnOpenPanelByName;
            UISignal.OnOpenPanel -= OnOpenPanel;
            UISignal.OnClosePanelByType -= OnClosePanelByType;
            UISignal.OnClosePanelByLayer -= OnClosePanelByLayer;
            UISignal.OnCloseTopPanel -= OnCloseTopPanel;
            UISignal.OnCloseAllPanels -= OnCloseAllPanels;
            UISignal.OnSetPanelState -= OnSetPanelState;
            UISignal.OnEscapeKeyPressed -= OnEscapeKeyPressed;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void Start()
        {
            OnOpenPanel(UISignal.OnGetUIPanelData(UIPanelTypes.Menu));
        }

        #region Signal Methods
        
        private void OnOpenPanelByName(string panelName)
        {
            Debug.Log("OnOpenPanelByName: " + panelName);
        }
        
        private void OnOpenPanel(UIPanelData data)
        {
            Debug.Log("OnOpenPanel: " + data.panelType);
            // TODO: Later, a data.layer control can be added here.

            
            if (GetTheTopPanelData().panelType == data.panelType)
            {
                if (data.panelLayer != 0)
                {
                    print("Trying to open the same panel.");
                    OnEscapeKeyPressed();
                    return;
                }
            }
            
            // TODO: If user forgets to add Menu panel to openablePanels, this part should be updated. Otherwise, it will be a problem.
            if (!GetTheTopPanelData().openablePanels.Contains(data.panelType) && GetTheTopPanelData().panelType != data.panelType)
            {
                if (data.panelLayer != 0)
                {
                }
                print("Openable panel değil.");
                return;
            }
            
            // TODO: If user wants to edit Menu panel to IsDisableable, this part should be updated. Otherwise, it will be a problem.
            if (GetTheTopPanelData().isDisableable && !GetTheTopPanelData().exceptionPanelsForOpenable.Contains(data.panelType))
            {
                layers[GetTheTopPanelData().panelLayer].transform.GetChild(0).gameObject.SetActive(false);
            }
            
            GameObject panel = Instantiate(data.panelPrefab, layers[data.panelLayer].transform);
        }
        
        private void OnClosePanelByType(UIPanelData data)
        {
            Debug.Log("OnClosePanelByType: " + data.panelType);
        }
        
        private void OnClosePanelByLayer(short layer)
        {
            Debug.Log("OnClosePanelByLayer: " + layer);
            
            if (layers[layer].transform.childCount > 0)
            {
                for (int i = 0; i < layers[layer].transform.childCount; i++)
                {
                    Destroy(layers[layer].transform.GetChild(i).gameObject);
                }
            }
        }
        
        private void OnCloseTopPanel()
        {
            Debug.Log("OnCloseTopPanel");
            
            for (int i = layers.Count - 1; i > 0; i--)
            {
                if (layers[i].transform.childCount > 0)
                {
                    OnClosePanelByLayer((short)i);
                    break;
                }
            }
        }
        
        private void OnCloseAllPanels()
        {
            Debug.Log("OnCloseAllPanels");
            
            foreach (var layer in layers)
            {
                for (int i = 0; i < layer.transform.childCount; i++)
                {
                    Destroy(layer.transform.GetChild(i).gameObject);
                }
            }
        }
        
        private void OnSetPanelState(bool state)
        {
            Debug.Log("OnSetPanelState: " + state);
        }
        
        private void OnEscapeKeyPressed()
        {
            Debug.Log("OnEscapeKeyPressed");
            
            if (GetTheTopPanelData().escapePanel == UIPanelTypes.None)
            {
                layers[GetPreviousPanelData(GetTheTopPanelData().panelLayer).panelLayer].transform.GetChild(0).gameObject.SetActive(true);
                OnCloseTopPanel();
            }
            else
            {
                OnOpenPanel(UISignal.OnGetUIPanelData(GetTheTopPanelData().escapePanel));
            }
        }

        #endregion
        
        private UIPanelData GetPreviousPanelData(int layerValue)
        {
            for (int i = layerValue - 1; i >= 0; i--)
            {
                if (layers[i].transform.childCount > 0 && !layers[i].transform.GetChild(0).gameObject.activeSelf)
                {
                    // TODO: Getting panel name from panel object name is not a good idea. It should be stored in a variable or something.
                    string panelName = layers[i].transform.GetChild(0).name.Replace("Panel(Clone)", "").Trim();
                    print($"Lower panel: {panelName}");
                    return UISignal.OnGetUIPanelData((UIPanelTypes)Enum.Parse(typeof(UIPanelTypes), panelName));
                }
            }
            print($"No lower panel. {UISignal.OnGetUIPanelData(UIPanelTypes.Menu).panelName}");
            return UISignal.OnGetUIPanelData(UIPanelTypes.Menu);
        }
        
        private UIPanelData GetTheTopPanelData()
        {
            for (int i = layers.Count - 1; i >= 0; i--)
            {
                if (layers[i].transform.childCount > 0)
                {
                    // TODO: Getting panel name from panel object name is not a good idea. It should be stored in a variable or something.
                    string panelName = layers[i].transform.GetChild(0).name.Replace("Panel(Clone)", "").Trim();
                    print($"The top panel: {panelName}");
                    return UISignal.OnGetUIPanelData((UIPanelTypes)Enum.Parse(typeof(UIPanelTypes), panelName));
                }
            }
            print($"No the top panel: {UISignal.OnGetUIPanelData(UIPanelTypes.Menu).panelName}");
            return UISignal.OnGetUIPanelData(UIPanelTypes.Menu);
        }
    }
}