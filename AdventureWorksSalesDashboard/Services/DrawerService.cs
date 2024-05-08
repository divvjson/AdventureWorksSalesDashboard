namespace AdventureWorksSalesDashboard.Services
{
    public class DrawerService
    {
        private bool isOpen = true;

        public event Action? OnChange;

        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;
                NotifyStateChanged();
            }
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
