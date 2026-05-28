namespace CleanRefactor
{
    public class BuyShieldUseCase
    {
        private readonly IShopStorage shopStorage;
        private readonly ShopConfig shopConfig;

        public BuyShieldUseCase(IShopStorage shopStorage, ShopConfig shopConfig)
        {
            this.shopStorage = shopStorage;
            this.shopConfig = shopConfig;
        }

        public PurchaseResultDto Execute()
        {
            PlayerShopState state = shopStorage.Load();

            if (state.Coins < shopConfig.ShieldCost)
            {
                return new PurchaseResultDto(PurchaseStatus.NotEnoughCoins, state.Coins);
            }

            if (state.ShieldUses >= shopConfig.ShieldMaxUses)
            {
                return new PurchaseResultDto(PurchaseStatus.MaxUsesReached, state.Coins);
            }

            state.Coins -= shopConfig.ShieldCost;
            state.ShieldUses++;

            shopStorage.Save(state);

            return new PurchaseResultDto(PurchaseStatus.Purchased, state.Coins);
        }
    }
}
