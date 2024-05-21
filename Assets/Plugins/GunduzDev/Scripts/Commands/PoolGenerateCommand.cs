using System.Collections.Generic;
using GD.Datas.ValueObjects;
using GD.Enums;
using UnityEngine;

namespace GD.Commands
{
    public struct PoolGenerateCommand
    {
        private List<PoolData> _poolData;
        private Transform _poolHolder;
        private GameObject _emptyObject;
        public PoolGenerateCommand(ref List<PoolData> poolData, ref Transform poolHolder, ref GameObject emptyObject)
        {
            _poolData = poolData;
            _poolHolder = poolHolder;
            _emptyObject = emptyObject;
        }

        public void Execute()
        {
            var poolList = _poolData;

            for (var i = 0; i < poolList.Count; i++)
            {
                _emptyObject = new GameObject();
                _emptyObject.transform.parent = _poolHolder;
                _emptyObject.name = poolList[i].objectType.ToString();

                for (var j = 0; j < poolList[i].poolSize; j++)
                {
                    var obj = Object.Instantiate(poolList[i].prefab, _poolHolder.GetChild(i));
                    obj.SetActive(false);
                }
            }
        }
    }
}
