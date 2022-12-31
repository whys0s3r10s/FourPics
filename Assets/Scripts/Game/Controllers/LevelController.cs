using System;

namespace FourPics
{
    public class LevelController : ILevelController
    {
        private readonly LevelContentData _levelContentData;
        private readonly ILevelRepository _levelRepository;

        public int UnlockedLevelNumber { get; private set; }

        public LevelController(LevelContentData levelContentData, ILevelRepository levelRepository)
        {
            _levelContentData = levelContentData ?? throw new ArgumentNullException(nameof(levelContentData));
            _levelRepository = levelRepository ?? throw new ArgumentNullException(nameof(levelRepository));

            Initialize();
        }

        public bool HasLevelsToPlay()
        {
            return GetLastLevelNumber() >= UnlockedLevelNumber;
        }

        public LevelData GetCurrentLevel()
        {
            return GetLevel(UnlockedLevelNumber);
        }

        public void MoveToNextLevel()
        {
            UnlockedLevelNumber++;

            _levelRepository.SaveUnlockedLevelNumber(UnlockedLevelNumber);
        }

        public LevelData GetLevel(int levelNumber)
        {
            if (levelNumber < 1)
                throw new ArgumentException("Level number can't be less than 1", nameof(levelNumber));

            if (levelNumber > _levelContentData.Levels.Count)
                throw new LevelNotFoundException($"Level {levelNumber} is not found." +
                    $" Total levels count: {_levelContentData.Levels.Count}");

            return _levelContentData.Levels[levelNumber - 1];
        }

        public int GetLastLevelNumber()
        {
            if (_levelContentData.Levels == null || _levelContentData.Levels.Count == 0)
                throw new InvalidOperationException("Levels are not defined");

            return _levelContentData.Levels.Count;
        }

        private void Initialize()
        {
            UnlockedLevelNumber = _levelRepository.GetUnlockedLevelNumber();
        }
    }
}