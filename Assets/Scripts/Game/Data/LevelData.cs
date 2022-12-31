using System;
using System.Collections.Generic;
using UnityEngine;

namespace FourPics
{
    [Serializable]
    [CreateAssetMenu(fileName = "Level", menuName = "Game/Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        public List<Sprite> Pics;

        public string Word;

        public string Letters;
    }
}
