namespace CleanRefactor
{
    public class GetShopStatusUseCase
    {
        private readonly IShopStorage shopStorage;
        private readonly ShopConfig shopConfig;

        public GetShopStatusUseCase(IShopStorage shopStorage, ShopConfig shopConfig)
        {
            this.shopStorage = shopStorage;
            this.shopConfig = shopConfig;
        }

        public ShopStatusDto Execute()
        {
            PlayerShopState state = shopStorage.Load();

            bool canBuyBomb =
                state.Coins >= shopConfig.BombCost &&
                state.BombUses < shopConfig.BombMaxUses;

            bool canBuyShield =
                state.Coins >= shopConfig.ShieldCost &&
                state.ShieldUses < shopConfig.ShieldMaxUses;

            bool canBuyDoubleCoins =
                state.Coins >= shopConfig.DoubleCoinsCost &&
                state.PlayerLevel >= shopConfig.DoubleCoinsRequiredLevel &&
                !state.HasDoubleCoins;

            return new ShopStatusDto(
                state.Coins,
                canBuyBomb,
                canBuyShield,
                canBuyDoubleCoins);
        }
    }
}
