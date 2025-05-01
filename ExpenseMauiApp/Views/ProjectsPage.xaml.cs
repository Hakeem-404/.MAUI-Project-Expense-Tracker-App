using ExpenseMauiApp.Models;
using ExpenseMauiApp.Services;
using System.Collections.ObjectModel;

namespace ExpenseMauiApp.Views;

public partial class ProjectsPage : ContentPage
{
    private CloudService _cloudService;
    public ObservableCollection<Project> Projects { get; set; }

    public ProjectsPage()
    {
        InitializeComponent();
        _cloudService = new CloudService();
        Projects = new ObservableCollection<Project>();
        projectsCollectionView.ItemsSource = Projects;
        LoadProjects();
    }

    private async void LoadProjects()
    {
        var projects = await _cloudService.GetProjectsAsync();
        Projects.Clear();
        foreach (var project in projects)
        {
            Projects.Add(project);
        }
    }

    private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(query))
        {
            LoadProjects();
        }
        else
        {
            var projects = await _cloudService.SearchProjectsAsync(query);
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }
    }
}
