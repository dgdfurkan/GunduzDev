using System.Collections.Generic;
using GD.Commands;
using GD.Datas.ValueObjects;
using GD.Signals;
using GD.Enums;
using Unity.VisualScripting;
using UnityEngine;
using IPoolable = GD.Interfaces.IPoolable;

namespace GD.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables

        //

        #endregion

        #region Private Variables

        private const string PathOfData = "GunduzDev/Datas/PoolDatas";
        private List<PoolData> _datas = new List<PoolData>();
        private PoolGenerateCommand _poolGenerateCommand;
        private Transform _poolHolder;
        private GameObject _emptyObject;
        
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PoolSignal.OnGetPool += OnGetPool;
            PoolSignal.OnReturnPool += OnReturnPool;
            PoolSignal.OnGetPoolCount += OnGetPoolCount;
        }

        private void UnsubscribeEvents()
        {
            PoolSignal.OnGetPool -= OnGetPool;
            PoolSignal.OnReturnPool -= OnReturnPool;
            PoolSignal.OnGetPoolCount = OnGetPoolCount;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void Awake()
        {
            _datas = GetDatas();
        }
        
        private void Start()
        {
            GeneratePool();
        }

        #region Data Methods

        private List<PoolData> GetDatas()
        {
            if (_datas.Count is 0)
            {
                CD_Pool[] datas = Resources.LoadAll<CD_Pool>(PathOfData);
                foreach (var data in datas)
                {
                    if (data.Data.objectType is not PoolTypes.None)
                    {
                        _datas.Add(data.GetData());
                    }
                }
            }
            return _datas;
        }
        
        public List<PoolData> SendDatasToController() => _datas;

        void CreatePool()
        {
            foreach (var data in _datas)
            {
                GameObject poolContainer = new GameObject(data.objectType.ToString());
                poolContainer.transform.SetParent(transform);

                print($"Pool Type: {data.objectType} - Pool Size: {data.poolSize} - Is Expandable: {data.isExpandable}");
                for (int i = 0; i < data.poolSize; i++)
                {
                    GameObject obj = Instantiate(data.prefab, poolContainer.transform);
                    obj.name = data.objectType.ToString() + i;
                    obj.SetActive(false);
                }
            }
        }
        
        private void GeneratePool()
        {
            _poolGenerateCommand = new PoolGenerateCommand(ref _datas, ref _poolHolder, ref _emptyObject);
            _poolGenerateCommand.Execute();
        }
        
        #endregion

        GameObject OnGetPool(PoolTypes poolType, Transform parentTransform)
        {
            // Havuzdaki ilgili PoolData nesnesini bul
            PoolData poolData = _datas.Find(data => data.objectType == poolType);

            // Havuzdan bir nesne al ve parent'ını belirtilen Transform nesnesi olarak ayarla
            GameObject obj = poolData.prefab;
            //obj.transform.SetParent(parentTransform);
            return obj;
        }
        
        void OnReturnPool(GameObject pooledObject, PoolTypes poolType)
        {
            // Havuzdaki ilgili PoolData nesnesini bul
            PoolData poolData = _datas.Find(data => data.objectType == poolType);
            
            // Havuzdaki nesneyi pasif hale getir
            pooledObject.SetActive(false);
        }
        
        private int OnGetPoolCount(PoolTypes poolType)
        {
            // Havuzdaki ilgili PoolData nesnesini bul
            PoolData poolData = _datas.Find(data => data.objectType == poolType);

            // Havuzdaki nesnelerin sayısını döndür
            return poolData.poolSize;
        }
    }
}