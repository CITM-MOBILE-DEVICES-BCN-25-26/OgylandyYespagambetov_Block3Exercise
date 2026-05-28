using UnityEngine;

namespace CleanRefactor
{
    public class UnityShopAudioPlayer : IShopAudioPlayer
    {
        private readonly AudioSource audioSource;

        public UnityShopAudioPlayer(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public void PlayPurchaseSuccess()
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
