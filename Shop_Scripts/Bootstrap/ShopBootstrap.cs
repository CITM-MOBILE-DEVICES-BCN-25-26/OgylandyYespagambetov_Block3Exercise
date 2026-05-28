using UnityEngine;

namespace CleanRefactor
{
    public class ShopBootstrap : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private ShopView shopView;

        [Header("Audio")]
        [SerializeField] private AudioSource purchaseAudioSource;

        [Header("Config")]
        [SerializeField] private int bombCost = 100;
        [SerializeField] private int bombMaxUses = 3;
        [SerializeField] private int shieldCost = 150;
        [SerializeField] private int shieldMaxUses = 2;
        [SerializeField] private int doubleCoinsCost = 300;
        [SerializeField] private int doubleCoinsRequiredLevel = 5;

        private ShopPresenter shopPresenter;

        private void Start()
        {
          


            ShopConfig shopConfig = new ShopConfig(
                bombCost,
                bombMaxUses,
                shieldCost,
                shieldMaxUses,
                doubleCoinsCost,
                doubleCoinsRequiredLevel);

            IShopStorage shopStorage = new PlayerPrefsShopStorage();
            IShopAudioPlayer shopAudioPlayer = new UnityShopAudioPlayer(purchaseAudioSource);

            GetShopStatusUseCase getShopStatusUseCase = new GetShopStatusUseCase(shopStorage, shopConfig);
            BuyBombUseCase buyBombUseCase = new BuyBombUseCase(shopStorage, shopConfig);
            BuyShieldUseCase buyShieldUseCase = new BuyShieldUseCase(shopStorage, shopConfig);
            BuyDoubleCoinsUseCase buyDoubleCoinsUseCase = new BuyDoubleCoinsUseCase(shopStorage, shopConfig);

            shopPresenter = new ShopPresenter(
                shopView,
                shopAudioPlayer,
                getShopStatusUseCase,
                buyBombUseCase,
                buyShieldUseCase,
                buyDoubleCoinsUseCase);

            shopView.Initialize(shopPresenter);
            shopPresenter.Initialize();
        }
    }
}