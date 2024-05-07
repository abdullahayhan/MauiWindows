using MudBlazor;

namespace MauiApp2.Shared
{
    public partial class MainLayout
    {
        public bool _drawerOpen = true;

        protected override void OnInitialized()
        {
            StateHasChanged();
        }

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
