namespace ZaptimeChatApp.Client.Services
{
    public class OverlayService
    {
        public event Func<Task> OnShowOverlay;
        public event Func<Task> OnCloseOverlay;

        public async Task ShowOverlay()
        {
            if (OnShowOverlay != null)
            {
                await OnShowOverlay.Invoke();
            }
        }

        public async Task CloseOverlay()
        {
            if (OnCloseOverlay != null)
            {
                await OnCloseOverlay.Invoke();
            }
        }
    }
}
