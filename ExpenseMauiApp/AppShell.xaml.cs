using ExpenseMauiApp.Views;

namespace ExpenseMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes so we can navigate to these pages
        Routing.RegisterRoute(nameof(ProjectDetailPage), typeof(ProjectDetailPage));
        Routing.RegisterRoute(nameof(AddExpensePage), typeof(AddExpensePage));
        Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));

        // Navigation event handlers
        this.Navigated += OnNavigated;
    }

    private async void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        // Check if we're navigating back to the FavoritesPage
        if (e.Current.Location.ToString().EndsWith("//FavoritesPage") && 
            e.Previous.Location.ToString().Contains("ProjectDetailPage"))
        {
            // We're returning to FavoritesPage from ProjectDetailPage
            if (Shell.Current.CurrentPage is Views.FavoritesPage favoritesPage)
            {
                // Refresh the favorites list
                await favoritesPage.RefreshFavoritesAsync();
            }
        }
    }
}
