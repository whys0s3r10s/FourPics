using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace FourPics
{
    public class MainView : ViewBase<MainViewModel>
    {
        public override ViewNames ViewName => ViewNames.Main;

        public Text currentLevel;
        public GameObject allLevelsCompleted;
        public GameObject playButton;

        public override void Initialize(MainViewModel viewModel)
        {
            base.Initialize(viewModel);

            Assert.IsNotNull(currentLevel);
            Assert.IsNotNull(allLevelsCompleted);
            Assert.IsNotNull(playButton);
        }

        public void OnPlay()
        {
            ViewModel.OnPlay();
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            allLevelsCompleted.SetActive(ViewModel.AllLevelsCompleted);

            currentLevel.text = $"LEVEL {ViewModel.CurrentLevelNumber}";

            playButton.SetActive(!ViewModel.AllLevelsCompleted);
        }
    }
}