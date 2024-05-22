using System;
using DG.Tweening;
using GD.Enums;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GD.Datas.ValueObjects
{
    [Serializable]
    public struct UIPanelData
    {
        public string panelName;
        public UIPanelTypes type;
        public GameObject panelPrefab;
        public bool isDisableable;
        [Range((int)0,(int)5)]
        public short panelLayer;
        public UIPanelTypes[] openablePanels;
        [ShowIf("ShowException")]
        public UIPanelTypes[] exceptionPanelsForOpenable;
        public Ease panelOpeningEase;
        public Ease panelClosingEase;
        public UIPanelTypes escapePanel;
        
        private bool ShowException() => isDisableable == true;
    }
}