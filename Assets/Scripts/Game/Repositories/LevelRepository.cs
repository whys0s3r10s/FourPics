namespace FourPics
{
    public class LevelRepository : RepositoryBase, ILevelRepository
    {
        private const string UnlockedLevelNumberKey = nameof(UnlockedLevelNumberKey);

        public LevelRepository(IStorage storage) : base(storage)
        {
        }

        public int GetUnlockedLevelNumber()
        {
            return Storage.GetInt(UnlockedLevelNumberKey, 1);
        }

        public void SaveUnlockedLevelNumber(int value)
        {
            Storage.SetInt(UnlockedLevelNumberKey, value);
        }
    }
}