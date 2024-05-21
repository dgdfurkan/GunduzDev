using System;
using GD.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace GD.Signals
{
    public static class PoolSignal
    {
        //public static UnityAction<UIPanelData> onOpenPanel = delegate {  };
        //public static Func<UIPanelTypes, UIPanelData> onGetUIPanelData;
        
        public static Func<PoolTypes, Transform, GameObject> OnGetPool = delegate { return null; };
        public static Func<PoolTypes, int> OnGetPoolCount = delegate { return 0; };
        public static UnityAction<GameObject, PoolTypes> OnReturnPool = delegate {  };
    }
}