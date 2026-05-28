namespace CleanRefactor
{
    public interface IShopStorage
    {
        PlayerShopState Load();
        void Save(PlayerShopState state);
    }
}