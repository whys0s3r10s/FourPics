using UnityEditor;
using UnityEngine;

namespace FourPics
{
    public class EditorMenu
    {
        [MenuItem("Four Pics/Clear player prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();

            Debug.Log("PlayerPrefs cleared");
        }
    }
}
