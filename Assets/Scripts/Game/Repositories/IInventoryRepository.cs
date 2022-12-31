namespace FourPics
{
    public interface IInventoryRepository
    {
        int GetCoins();
        void SaveCoins(int value);
    }
}