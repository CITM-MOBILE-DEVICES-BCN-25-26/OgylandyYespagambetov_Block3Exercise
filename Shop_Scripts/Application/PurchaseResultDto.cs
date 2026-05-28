namespace CleanRefactor
{
    public class PurchaseResultDto
    {
        public PurchaseStatus Status { get; }
        public int Coins { get; }

        public PurchaseResultDto(PurchaseStatus status, int coins)
        {
            Status = status;
            Coins = coins;
        }
    }
}