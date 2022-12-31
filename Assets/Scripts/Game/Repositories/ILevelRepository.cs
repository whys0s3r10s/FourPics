namespace FourPics
{
    public interface ILevelRepository
    {
        int GetUnlockedLevelNumber();

        void SaveUnlockedLevelNumber(int value);
    }
}