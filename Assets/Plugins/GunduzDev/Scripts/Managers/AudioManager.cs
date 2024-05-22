using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GD.Datas.ValueObjects;
using GD.Enums;
using GD.Extentions;
using GD.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace GD.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables

        //

        #endregion

        #region Private Variables

        private const string PathOfData = "GunduzDev/Datas/AudioDatas";
        private List<AudioData> _datas = new List<AudioData>();
        
        private AudioSource _audioSource;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AudioSignal.OnGetData += OnGetData;
        }

        private void UnsubscribeEvents()
        {
            AudioSignal.OnGetData -= OnGetData;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void Awake()
        {
            _datas = GetDatas();
        }
        
        private List<AudioData> GetDatas()
        {
            return _datas = new List<AudioData>(Resources.LoadAll<CD_Audio>(PathOfData)
                .Select(item => item.Data)
                .ToList());
        }
        
        private AudioData OnGetData(AudioTypes type)
        {
            return _datas.Find(x => x.type == type);
        }
    }
}