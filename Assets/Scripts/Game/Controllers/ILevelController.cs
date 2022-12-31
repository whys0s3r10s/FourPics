namespace FourPics
{
    public interface ILevelController
    {
        int UnlockedLevelNumber { get; }

        LevelData GetCurrentLevel();

        int GetLastLevelNumber();

        LevelData GetLevel(int levelNumber);

        bool HasLevelsToPlay();

        void MoveToNextLevel();
    }
}