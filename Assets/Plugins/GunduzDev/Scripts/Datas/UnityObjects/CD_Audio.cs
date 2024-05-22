using UnityEngine;

namespace GD.Datas.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_Audio", menuName = "GunduzDevSO/CD_Audio", order = 0)]
    public class CD_Audio : ScriptableObject
    {
        public AudioData Data;
        public AudioData GetData() => Data;
    }
}