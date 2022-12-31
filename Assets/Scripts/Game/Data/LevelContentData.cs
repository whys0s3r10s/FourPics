using System;
using System.Collections.Generic;
using UnityEngine;

namespace FourPics
{
    [Serializable]
    [CreateAssetMenu(fileName = "Levels", menuName = "Content Data/Levels")]
    public class LevelContentData : ScriptableObject
    {
        public List<LevelData> Levels;        
    }
}