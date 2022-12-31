using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class GameView : ViewBase<GameViewModel>
    {
        public override ViewNames ViewName => ViewNames.Game;

        [SerializeField]
        private Text currentLevel;
        [SerializeField]
        private List<Image> pics;
        [SerializeField]
        private TargetWordControl targetWordControl;
        [SerializeField]
        private HintsControl hintsControl;
        [SerializeField]
        private LettersControl lettersControl;

        public override void Initialize(GameViewModel viewModel)
        {
            base.Initialize(viewModel);

            Assert.IsNotNull(currentLevel);
            Assert.IsNotNull(pics);
            Assert.IsTrue(pics.Count == 4);
            Assert.IsNotNull(targetWordControl);
            Assert.IsNotNull(hintsControl);
            Assert.IsNotNull(lettersControl);

            targetWordControl.Initialize();
            targetWordControl.OnLetterClicked += OnTargetWordLetterClicked;

            lettersControl.Initialize();
            lettersControl.OnLetterClicked += OnLetterClicked;

            hintsControl.OnHintClicked += OnHintClicked;

            ViewModel.OnLettersChanged += OnLettersChanged;
            ViewModel.OnWrongWord += OnWrongWord;
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            // Current level text
            currentLevel.text = $"LEVEL {ViewModel.CurrentLevelNumber}";

            // Assign four level pictures
            for (int i = 0; i < ViewModel.LevelData.Pics.Count; i++)
            {
                pics[i].sprite = ViewModel.LevelData.Pics[i];
            }

            hintsControl.UpdateRender(ViewModel.HintPrices);

            targetWordControl.Setup(ViewModel.LevelData.Word, ViewModel.Letters);
            lettersControl.Setup(ViewModel.Letters);
        }

        public void OnBack()
        {
            ViewModel.OnBack();
        }

        private void OnTargetWordLetterClicked(Letter letter)
        {
            ViewModel.DetachLetter(letter);
        }

        private void OnLetterClicked(Letter letter)
        {
            ViewModel.AttachLetter(letter);
        }

        private void OnLettersChanged()
        {
            targetWordControl.UpdateRender();
            lettersControl.UpdateRender();
        }

        private void OnWrongWord()
        {
            targetWordControl.AnimateWrong();
        }

        private void OnHintClicked(HintType hintType)
        {
            ViewModel.OnUseHint(hintType);
        }
    }
}