namespace CleanRefactor
{
    public class BuyDoubleCoinsUseCase
    {
        private readonly IShopStorage shopStorage;
        private readonly ShopConfig shopConfig;

        public BuyDoubleCoinsUseCase(IShopStorage shopStorage, ShopConfig shopConfig)
        {
            this.shopStorage = shopStorage;
            this.shopConfig = shopConfig;
        }

        public PurchaseResultDto Execute()
        {
            PlayerShopState state = shopStorage.Load();

            if (state.Coins < shopConfig.DoubleCoinsCost)
            {
                return new PurchaseResultDto(PurchaseStatus.NotEnoughCoins, state.Coins);
            }

            if (state.PlayerLevel < shopConfig.DoubleCoinsRequiredLevel)
            {
                return new PurchaseResultDto(PurchaseStatus.RequiredLevelNotReached, state.Coins);
            }

            if (state.HasDoubleCoins)
            {
                return new PurchaseResultDto(PurchaseStatus.AlreadyOwned, state.Coins);
            }

            state.Coins -= shopConfig.DoubleCoinsCost;
            state.HasDoubleCoins = true;

            shopStorage.Save(state);

            return new PurchaseResultDto(PurchaseStatus.Purchased, state.Coins);
        }
    }
}