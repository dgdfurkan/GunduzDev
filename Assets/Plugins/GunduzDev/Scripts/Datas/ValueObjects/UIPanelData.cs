using System;
using GD.Enums;
using UnityEngine;

namespace GD.Datas.ValueObjects
{
    [Serializable]
    public struct UIPanelData
    {
        public string panelName;
        public UIPanelTypes panelType;
        public GameObject panelPrefab;
        public bool isDisableable;
        [Range((int)0,(int)5)]
        public short panelLayer;
        public UIPanelTypes[] openablePanels;
        //[ShowIf("ShouldShowESCPanel")]
        public UIPanelTypes[] exceptionPanelsForOpenable;
        ///public Ease PanelOpeningEase;
        ///public Ease PanelClosingEase;
        public UIPanelTypes escapePanel;
        
        private bool ShowException() => isDisableable is true;
    }
}