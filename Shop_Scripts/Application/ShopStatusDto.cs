namespace CleanRefactor
{
    public class ShopStatusDto
    {
        public int Coins { get; }
        public bool CanBuyBomb { get; }
        public bool CanBuyShield { get; }
        public bool CanBuyDoubleCoins { get; }

        public ShopStatusDto(
            int coins,
            bool canBuyBomb,
            bool canBuyShield,
            bool canBuyDoubleCoins)
        {
            Coins = coins;
            CanBuyBomb = canBuyBomb;
            CanBuyShield = canBuyShield;
            CanBuyDoubleCoins = canBuyDoubleCoins;
        }
    }
}