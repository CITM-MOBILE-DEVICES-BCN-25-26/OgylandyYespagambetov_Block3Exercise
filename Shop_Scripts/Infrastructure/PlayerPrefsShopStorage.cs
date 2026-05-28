using UnityEngine;

namespace CleanRefactor
{
    public class PlayerPrefsShopStorage : IShopStorage
    {
        private const string CoinsKey = "Coins";
        private const string PlayerLevelKey = "PlayerLevel";
        private const string BombUsesKey = "BombUses";
        private const string ShieldUsesKey = "ShieldUses";
        private const string HasDoubleCoinsKey = "HasDoubleCoins";

        public PlayerShopState Load()
        {
            return new PlayerShopState
            {
                Coins = PlayerPrefs.GetInt(CoinsKey, 500),
                PlayerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 1),
                BombUses = PlayerPrefs.GetInt(BombUsesKey, 0),
                ShieldUses = PlayerPrefs.GetInt(ShieldUsesKey, 0),
                HasDoubleCoins = PlayerPrefs.GetInt(HasDoubleCoinsKey, 0) == 1
            };
        }

        public void Save(PlayerShopState state)
        {
            PlayerPrefs.SetInt(CoinsKey, state.Coins);
            PlayerPrefs.SetInt(PlayerLevelKey, state.PlayerLevel);
            PlayerPrefs.SetInt(BombUsesKey, state.BombUses);
            PlayerPrefs.SetInt(ShieldUsesKey, state.ShieldUses);
            PlayerPrefs.SetInt(HasDoubleCoinsKey, state.HasDoubleCoins ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}