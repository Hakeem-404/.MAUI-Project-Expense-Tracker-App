using ExpenseMauiApp.Models;
using ExpenseMauiApp.Services;
using System.Collections.ObjectModel;

namespace ExpenseMauiApp.Views;

public partial class FavoritesPage : ContentPage
{
    private CloudService _cloudService;
    public ObservableCollection<Project> FavoriteProjects { get; set; } = new();
    
    public FavoritesPage()
    {
        InitializeComponent();
        _cloudService = new CloudService();
        projectsCollectionView.ItemsSource = FavoriteProjects;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadFavoriteProjectsAsync();
    }
    
    private async Task LoadFavoriteProjectsAsync()
    {
        try
        {
            // Show the loading indicator
            activityIndicator.IsRunning = true;
            
            FavoriteProjects.Clear();
            var allProjects = await _cloudService.GetProjectsAsync();
            
            // Load the favorite status
            await FavoritesManager.LoadFavoritesIntoProjectsAsync(allProjects);
            
            // Only add favorites to the collection
            foreach (var p in allProjects.Where(p => p.IsFavorite))
            {
                FavoriteProjects.Add(p);
            }
            
            // Only show message if there are no favorites (using EmptyView now)
            // No need for the explicit DisplayAlert
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load favorites: {ex.Message}", "OK");
        }
        finally
        {
            // Hide the loading indicator
            activityIndicator.IsRunning = false;
        }
    }
    
    private async void OnProjectSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Project selectedProject)
        {
            // Clear selection
            projectsCollectionView.SelectedItem = null;
            
            // Navigate to project details
            var navParams = new Dictionary<string, object>
            {
                { "SelectedProject", selectedProject }
            };
            await Shell.Current.GoToAsync(nameof(ProjectDetailPage), true, navParams);
        }
    }
    
    // Add a refresh method that can be called when returning to this page
    public async Task RefreshFavoritesAsync()
    {
        await LoadFavoriteProjectsAsync();
    }

    private async void OnItemTapped(object sender, EventArgs e)
{
    // Get the Frame that was tapped
    if (sender is Frame frame && frame.BindingContext is Project tappedProject)
    {
        // Optionally clear selection from the CollectionView
        projectsCollectionView.SelectedItem = null;

        // Navigate to the project details page with the tapped project
        var navParams = new Dictionary<string, object>
        {
            { "SelectedProject", tappedProject }
        };
        await Shell.Current.GoToAsync(nameof(ProjectDetailPage), true, navParams);
    }
}

}