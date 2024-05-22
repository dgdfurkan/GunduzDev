using GD.Datas.ValueObjects;
using UnityEngine.Events;
using System;
using GD.Enums;

namespace GD.Signals
{
    public static class AudioSignal
    {
        //public static UnityAction<UIPanelData> onOpenPanel = delegate {  };
        //public static Func<UIPanelTypes, UIPanelData> onGetUIPanelData;
        
        public static UnityAction<AudioData> OnPlayAudio = delegate {  };
        
        public static Func<AudioTypes, AudioData> OnGetData;
    }
}