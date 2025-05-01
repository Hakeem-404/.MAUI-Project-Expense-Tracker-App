using ExpenseMauiApp.Models;
using ExpenseMauiApp.Services;
using System.Collections.ObjectModel;

namespace ExpenseMauiApp.Views;

public partial class MainPage : ContentPage
{
    private CloudService _cloudService;
    
    public ObservableCollection<Project> Projects { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        _cloudService = new CloudService();
        BindingContext = this;
        // projectsCollectionView.ItemsSource = Projects;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProjectsAsync();
    }

    private async Task LoadProjectsAsync()
    {
        try
        {
            // Show loading indicator
            activityIndicator.IsRunning = true;
            
            Projects.Clear();
            var projects = await _cloudService.GetProjectsAsync();
            
            if (projects.Count == 0)
            {
                // Show message about no projects
                await DisplayAlert("No Projects", 
                    "No projects found in the cloud database.", 
                    "OK");
            }

            // Load the favorite status
            await FavoritesManager.LoadFavoritesIntoProjectsAsync(projects);
            
            foreach (var p in projects)
            {
                Projects.Add(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load projects: {ex.Message}", "OK");
        }
        finally
        {
            // Hide loading indicator
            activityIndicator.IsRunning = false;
        }
    }

    private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(query))
        {
            // Reload all
            await LoadProjectsAsync();
        }
        else
        {
            // Filter
            var filtered = await _cloudService.SearchProjectsAsync(query);
            Projects.Clear();
            foreach (var p in filtered)
            {
                Projects.Add(p);
            }
        }
    }

   private async void OnProjectSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Project selectedProject)
        {
            // Clear selection (optional)
            projectsCollectionView.SelectedItem = null;

            // Navigate to detail page, passing the Project as a route parameter
            var navParams = new Dictionary<string, object>
            {
                { "SelectedProject", selectedProject }
            };
            await Shell.Current.GoToAsync(nameof(ProjectDetailPage), true, navParams);
        }
    }



    private async void OnFrameTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Project selectedProject)
        {
            // Navigate to detail page, passing the Project as a route parameter
            var navParams = new Dictionary<string, object>
            {
                { "SelectedProject", selectedProject }
            };
            await Shell.Current.GoToAsync(nameof(ProjectDetailPage), true, navParams);
        }
    }
}