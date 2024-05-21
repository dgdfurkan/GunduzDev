using UnityEngine;

namespace GD.Datas.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "GunduzDevSO/CD_Pool", order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public PoolData Data;
        public PoolData GetData() => Data;
    }
}