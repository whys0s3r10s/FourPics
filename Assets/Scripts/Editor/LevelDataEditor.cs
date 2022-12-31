using UnityEditor;
using UnityEngine;

namespace FourPics
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : Editor
    {
        private readonly WordLetterGenerator _wordLetterGenerator;

        private const string _defaultAlphabet =
            "AAAAAABBCCCDDEEEEEEEFFFFGGHHIIIIIJKKKKKLLLLLMMMMMMNNNNNNOOOOOOOOOPPPPPQRRRRSSSSSTTTTTUUUUUVVWWWWXYYYZ";

        private const int _defaultMaxWordLength = 12;

        public LevelDataEditor()
        {
            _wordLetterGenerator = new WordLetterGenerator(_defaultAlphabet, _defaultMaxWordLength);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (LevelData)target;

            if (GUILayout.Button("Generate Letters", GUILayout.Height(40)))
            {
                script.Letters = _wordLetterGenerator.GenerateLetters(script.Word);
            }
        }
    }
}