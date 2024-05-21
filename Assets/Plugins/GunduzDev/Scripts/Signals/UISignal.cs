using System;
using GD.Datas.ValueObjects;
using GD.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace GD.Signals
{
    public static class UISignal
    {
        //public UnityAction<UIPanelData> onOpenPanel = delegate {  };
        //public Func<UIPanelTypes, UIPanelData> onGetUIPanelData;
        
        public static UnityAction<string> OnOpenPanelByName = delegate {  };
        public static UnityAction<UIPanelData> OnOpenPanel = delegate {  };
        public static UnityAction<UIPanelData> OnClosePanelByType = delegate {  };
        public static UnityAction<short> OnClosePanelByLayer = delegate {  };
        public static UnityAction OnCloseTopPanel = delegate{  };
        public static UnityAction OnCloseAllPanels = delegate {  };
        public static UnityAction<bool> OnSetPanelState = delegate {  };
        public static UnityAction OnEscapeKeyPressed = delegate {  };
        public static UnityAction<UIEventSubscriptionTypes> OnUIEventSubscription = delegate {  };
        
        public static Func<UIPanelTypes, UIPanelData> OnGetUIPanelData;
    }
}