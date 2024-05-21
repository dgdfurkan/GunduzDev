using System;
using GD.Enums;
using GD.Interfaces;
using UnityEngine;

namespace GD.Datas.ValueObjects
{
    [Serializable]
    public struct PoolData
    {
        [Header("Pool Data")]
        // [Tooltip("Name of the object in the pool")]
        // public string objectName;

        [Tooltip("Type of the object in the pool")]
        public PoolTypes objectType;

        [Tooltip("Prefab of the object to be pooled")]
        public GameObject prefab;

        [Tooltip("Initial size of the pool")]
        [Range(5, 20)]
        public int poolSize;

        [Tooltip("Can the pool size be expanded?")]
        public bool isExpandable;
    }
}