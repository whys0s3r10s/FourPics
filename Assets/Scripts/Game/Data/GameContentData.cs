using System;
using UnityEngine;

namespace FourPics
{
    [Serializable]
    [CreateAssetMenu(fileName = "Game", menuName = "Content Data/Game")]
    public class GameContentData : ScriptableObject
    {
        [Min(1)]
        public int CompletedLevelReward = 10;
    }
}