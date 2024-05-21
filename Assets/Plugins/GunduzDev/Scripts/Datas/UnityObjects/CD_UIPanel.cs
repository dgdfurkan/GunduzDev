using UnityEngine;

namespace GD.Datas.ValueObjects
{
    [CreateAssetMenu(fileName = "CD_UIPanel", menuName = "GunduzDevSO/CD_UIPanel", order = 0)]
    public class CD_UIPanel : ScriptableObject
    {
        public UIPanelData Data;
        public UIPanelData GetData() => Data;
    }
}