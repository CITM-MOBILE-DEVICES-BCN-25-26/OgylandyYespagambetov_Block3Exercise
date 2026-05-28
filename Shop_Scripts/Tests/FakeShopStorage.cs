namespace CleanRefactor.Tests
{
    public class FakeShopStorage : IShopStorage
    {
        private PlayerShopState state;

        public bool SaveWasCalled { get; private set; }

        public FakeShopStorage(PlayerShopState initialState)
        {
            state = initialState;
        }

        public PlayerShopState Load()
        {
            return state;
        }

        public void Save(PlayerShopState state)
        {
            this.state = state;
            SaveWasCalled = true;
        }
    }
}