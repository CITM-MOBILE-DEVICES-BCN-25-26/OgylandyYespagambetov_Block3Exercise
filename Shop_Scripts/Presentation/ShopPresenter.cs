namespace CleanRefactor
{
    public class ShopPresenter
    {
        private readonly IShopView shopView;
        private readonly IShopAudioPlayer shopAudioPlayer;
        private readonly GetShopStatusUseCase getShopStatusUseCase;
        private readonly BuyBombUseCase buyBombUseCase;
        private readonly BuyShieldUseCase buyShieldUseCase;
        private readonly BuyDoubleCoinsUseCase buyDoubleCoinsUseCase;

        public ShopPresenter(
            IShopView shopView,
            IShopAudioPlayer shopAudioPlayer,
            GetShopStatusUseCase getShopStatusUseCase,
            BuyBombUseCase buyBombUseCase,
            BuyShieldUseCase buyShieldUseCase,
            BuyDoubleCoinsUseCase buyDoubleCoinsUseCase)
        {
            this.shopView = shopView;
            this.shopAudioPlayer = shopAudioPlayer;
            this.getShopStatusUseCase = getShopStatusUseCase;
            this.buyBombUseCase = buyBombUseCase;
            this.buyShieldUseCase = buyShieldUseCase;
            this.buyDoubleCoinsUseCase = buyDoubleCoinsUseCase;
        }

        public void Initialize()
        {
            RefreshShop();
            shopView.SetFeedbackText("Select an item to buy.");
        }

        public void OnBuyBombClicked()
        {
            PurchaseResultDto result = buyBombUseCase.Execute();
            HandlePurchaseResult(result, "Bomb");
        }

        public void OnBuyShieldClicked()
        {
            PurchaseResultDto result = buyShieldUseCase.Execute();
            HandlePurchaseResult(result, "Shield");
        }

        public void OnBuyDoubleCoinsClicked()
        {
            PurchaseResultDto result = buyDoubleCoinsUseCase.Execute();
            HandlePurchaseResult(result, "Double Coins");
        }

        private void HandlePurchaseResult(PurchaseResultDto result, string itemName)
        {
            if (result.Status == PurchaseStatus.Purchased)
            {
                shopAudioPlayer.PlayPurchaseSuccess();
            }

            shopView.SetFeedbackText(GetFeedbackMessage(result.Status, itemName));
            RefreshShop();
        }

        private void RefreshShop()
        {
            ShopStatusDto status = getShopStatusUseCase.Execute();

            shopView.SetCoinsText("Coins: " + status.Coins);
            shopView.SetBombButtonInteractable(status.CanBuyBomb);
            shopView.SetShieldButtonInteractable(status.CanBuyShield);
            shopView.SetDoubleCoinsButtonInteractable(status.CanBuyDoubleCoins);
        }

        private string GetFeedbackMessage(PurchaseStatus status, string itemName)
        {
            switch (status)
            {
                case PurchaseStatus.Purchased:
                    return itemName + " purchased!";

                case PurchaseStatus.NotEnoughCoins:
                    return "Not enough coins for " + itemName;

                case PurchaseStatus.MaxUsesReached:
                    return itemName + " already at max uses";

                case PurchaseStatus.RequiredLevelNotReached:
                    return "Need level 5 for " + itemName;

                case PurchaseStatus.AlreadyOwned:
                    return itemName + " already purchased";

                default:
                    return "Unknown purchase result";
            }
        }
    }
}