namespace CleanRefactor
{
    public class ShopConfig
    {
        public int BombCost { get; }
        public int BombMaxUses { get; }
        public int ShieldCost { get; }
        public int ShieldMaxUses { get; }
        public int DoubleCoinsCost { get; }
        public int DoubleCoinsRequiredLevel { get; }

        public ShopConfig(
            int bombCost,
            int bombMaxUses,
            int shieldCost,
            int shieldMaxUses,
            int doubleCoinsCost,
            int doubleCoinsRequiredLevel)
        {
            BombCost = bombCost;
            BombMaxUses = bombMaxUses;
            ShieldCost = shieldCost;
            ShieldMaxUses = shieldMaxUses;
            DoubleCoinsCost = doubleCoinsCost;
            DoubleCoinsRequiredLevel = doubleCoinsRequiredLevel;
        }
    }
}