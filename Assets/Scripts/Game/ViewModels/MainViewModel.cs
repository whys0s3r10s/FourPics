using System;

namespace FourPics
{
    public class MainViewModel : ViewModelBase
    {
        private readonly LevelController _levelController;
        private readonly GameController _gameController;

        public int CurrentLevelNumber { get; private set; }
        public bool AllLevelsCompleted { get; private set; }

        public MainViewModel(
            NavigationController navigationController,
            LevelController levelController,
            GameController gameController) :
            base(navigationController)
        {
            _levelController = levelController ?? throw new ArgumentNullException(nameof(levelController));
            _gameController = gameController ?? throw new ArgumentNullException(nameof(gameController));
        }

        public void OnPlay()
        {
            if (_levelController.HasLevelsToPlay())
            {
                _gameController.PlayCurrentLevel();

                NavigateTo(ViewNames.Game);
            }
        }

        public override void OnShowing()
        {
            base.OnShowing();

            UpdateViewModel();
        }

        public override void OnBack()
        {
        }

        private void UpdateViewModel()
        {
            CurrentLevelNumber = _levelController.UnlockedLevelNumber;
            AllLevelsCompleted = !_levelController.HasLevelsToPlay();
        }
    }
}