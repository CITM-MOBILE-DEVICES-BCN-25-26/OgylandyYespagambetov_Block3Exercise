namespace CleanRefactor
{
    public class BuyBombUseCase
    {
        private readonly IShopStorage shopStorage;
        private readonly ShopConfig shopConfig;

        public BuyBombUseCase(IShopStorage shopStorage, ShopConfig shopConfig)
        {
            this.shopStorage = shopStorage;
            this.shopConfig = shopConfig;
        }

        public PurchaseResultDto Execute()
        {
            PlayerShopState state = shopStorage.Load();

            if (state.Coins < shopConfig.BombCost)
            {
                return new PurchaseResultDto(PurchaseStatus.NotEnoughCoins, state.Coins);
            }

            if (state.BombUses >= shopConfig.BombMaxUses)
            {
                return new PurchaseResultDto(PurchaseStatus.MaxUsesReached, state.Coins);
            }

            state.Coins -= shopConfig.BombCost;
            state.BombUses++;

            shopStorage.Save(state);

            return new PurchaseResultDto(PurchaseStatus.Purchased, state.Coins);
        }
    }
}