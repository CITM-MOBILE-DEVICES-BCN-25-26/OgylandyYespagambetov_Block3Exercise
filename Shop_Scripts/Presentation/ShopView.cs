using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CleanRefactor
{
    public class ShopView : MonoBehaviour, IShopView
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private Button bombButton;
        [SerializeField] private Button shieldButton;
        [SerializeField] private Button doubleCoinsButton;

        private ShopPresenter presenter;

        public void Initialize(ShopPresenter presenter)
        {
            this.presenter = presenter;

            bombButton.onClick.AddListener(OnBombButtonClicked);
            shieldButton.onClick.AddListener(OnShieldButtonClicked);
            doubleCoinsButton.onClick.AddListener(OnDoubleCoinsButtonClicked);
        }

        private void OnDestroy()
        {
            bombButton.onClick.RemoveListener(OnBombButtonClicked);
            shieldButton.onClick.RemoveListener(OnShieldButtonClicked);
            doubleCoinsButton.onClick.RemoveListener(OnDoubleCoinsButtonClicked);
        }

        private void OnBombButtonClicked()
        {
            presenter.OnBuyBombClicked();
        }

        private void OnShieldButtonClicked()
        {
            presenter.OnBuyShieldClicked();
        }

        private void OnDoubleCoinsButtonClicked()
        {
            presenter.OnBuyDoubleCoinsClicked();
        }

        public void SetCoinsText(string text)
        {
            coinsText.text = text;
        }

        public void SetFeedbackText(string text)
        {
            feedbackText.text = text;
        }

        public void SetBombButtonInteractable(bool interactable)
        {
            bombButton.interactable = interactable;
        }

        public void SetShieldButtonInteractable(bool interactable)
        {
            shieldButton.interactable = interactable;
        }

        public void SetDoubleCoinsButtonInteractable(bool interactable)
        {
            doubleCoinsButton.interactable = interactable;
        }
    }
}