namespace CleanRefactor
{
    public interface IShopView
    {
        void SetCoinsText(string text);
        void SetFeedbackText(string text);

        void SetBombButtonInteractable(bool interactable);
        void SetShieldButtonInteractable(bool interactable);
        void SetDoubleCoinsButtonInteractable(bool interactable);
    }
}
