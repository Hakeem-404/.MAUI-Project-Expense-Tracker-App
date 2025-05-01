using ExpenseMauiApp.Models;

namespace ExpenseMauiApp.Views;

[QueryProperty(nameof(SelectedProject), "SelectedProject")]
public partial class ProjectDetailPage : ContentPage
{
    private Project _selectedProject;

    public ProjectDetailPage()
    {
        InitializeComponent();
        // Set the BindingContext to this so we can use bindings properly
        BindingContext = this;
    }

    public Project SelectedProject
    {
        get => _selectedProject;
        set
        {
            _selectedProject = value;
            BindProjectDetails();
            OnPropertyChanged(nameof(FavoriteButtonText));
            OnPropertyChanged(nameof(FavoriteButtonColor));
        }
    }

    private void BindProjectDetails()
    {
        if (_selectedProject == null) return;
        
        // Set the data on the UI
        lblProjectName.Text = _selectedProject.ProjectName;
        lblProjectID.Text = _selectedProject.ProjectID;
        lblManager.Text = _selectedProject.Manager;
        lblStatus.Text = _selectedProject.ProjectStatus;
        lblBudget.Text = $"£{_selectedProject.Budget:N2}";
        lblStartDate.Text = _selectedProject.StartDate.ToString("dd/MM/yyyy");
        lblEndDate.Text = _selectedProject.EndDate.ToString("dd/MM/yyyy");
        lblDescription.Text = _selectedProject.ProjectDesc;
        
        // Show special requirements if present
        if (!string.IsNullOrWhiteSpace(_selectedProject.SpecialReq))
        {
            lblSpecialReq.Text = _selectedProject.SpecialReq;
            specialReqFrame.IsVisible = true;
        }
        
        // Show client info if present
        if (!string.IsNullOrWhiteSpace(_selectedProject.ClientInfo))
        {
            lblClientInfo.Text = _selectedProject.ClientInfo;
            clientInfoFrame.IsVisible = true;
        }
    }

    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        var navParams = new Dictionary<string, object>
        {
            { "ProjectID", _selectedProject.ProjectID }
        };
        await Shell.Current.GoToAsync(nameof(AddExpensePage), true, navParams);
    }

    public string FavoriteButtonText => _selectedProject?.IsFavorite == true ? "★ Unfavorite" : "☆ Favorite";
    
    // Fixed the double hash (#) in the color code
    public string FavoriteButtonColor => _selectedProject?.IsFavorite == true ? "#FFC107" : "#9E9E9E";
    
    private async void OnFavoriteClicked(object sender, EventArgs e)
    {
        if (_selectedProject == null) return;
        
        // Toggle the favorite status
        _selectedProject.IsFavorite = !_selectedProject.IsFavorite;

        // Update the UI
        OnPropertyChanged(nameof(FavoriteButtonText));
        OnPropertyChanged(nameof(FavoriteButtonColor));

        // Save the changes to preference
        await FavoritesManager.ToggleFavoriteAsync(_selectedProject);
    }
}