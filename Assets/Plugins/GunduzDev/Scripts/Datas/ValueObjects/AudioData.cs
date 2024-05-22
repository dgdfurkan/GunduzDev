using System;
using GD.Enums;
using UnityEngine;

namespace GD.Datas.ValueObjects
{
    [Serializable]
    public class AudioData
    {
        public string name;
        public AudioTypes type;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        public float pitch;
        public bool loop;
    }
}