using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GD.Extentions
{
    public abstract class ResourceLoader<TData, TCD> : MonoBehaviour where TData : class where TCD : Object
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables

        //

        #endregion

        #region Private Variables

        private string PathOfData = "GunduzDev/Datas/";
        private string PathOfDataSuffix = "s";
        protected List<TData> _dataList = new List<TData>();
        

        #endregion

        #endregion

        protected virtual void Awake()
        {
            PathOfData = PathOfData + typeof(TData).Name + PathOfDataSuffix;
            print(PathOfData);
            _dataList = GetDataList();
        }

        protected virtual void Start()
        {
            GetMyDatas();
        }

        private List<TData> GetDataList()
        {
            print("Sa");
            
            var loadedObjects = Resources.LoadAll<TCD>(PathOfData);
            
            print(loadedObjects);
            print(loadedObjects.Length);
            
            List<TData> dataList = new List<TData>();
            
            foreach (var loadedObject in loadedObjects)
            {
                print(loadedObject);
                print(dataList.Count);
                dataList.Add(loadedObject as TData);
            }
            
            return dataList;
        }

        // protected virtual TData OnGetData(TType type)
        // {
        //     return _dataList.Find(x => x.GetType() == type.GetType());
        //     
        // }
        
        private void GetMyDatas()
        {
            foreach (var data in _dataList)
            {
                Debug.Log(data + " is a " + typeof(TData).Name);
            }
        }
    }
}