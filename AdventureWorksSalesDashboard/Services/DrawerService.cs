namespace AdventureWorksSalesDashboard.Services
{
    public class DrawerService
    {
        public enum DrawerState { Closed, Opening, Open, Closing }
        private DrawerState state = DrawerState.Open;

        public event Action? OnChange;

        public bool IsOpen
        {
            get => state == DrawerState.Open || state == DrawerState.Opening;
        }

        public DrawerState State
        {
            get => state;
            private set
            {
                state = value;
                NotifyStateChanged();
            }
        }

        public async Task Open()
        {
            if (state == DrawerState.Closed || state == DrawerState.Closing)
            {
                State = DrawerState.Opening;
                await Task.Delay(225); // Wait for the animation time
                State = DrawerState.Open;
            }
        }

        public async Task Close()
        {
            if (state == DrawerState.Open || state == DrawerState.Opening)
            {
                State = DrawerState.Closing;
                await Task.Delay(225); // Wait for the animation time
                State = DrawerState.Closed;
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
