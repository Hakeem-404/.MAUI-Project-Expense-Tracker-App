using ExpenseMauiApp.Models;
using System.Text.Json;

namespace ExpenseMauiApp.Views;

public static class FavoritesManager
{
    private const string FavoritesKey = "favorite_projects";
    
    public static async Task<List<string>> GetFavoriteProjectIdsAsync()
    {
        string favoritesJson = await SecureStorage.GetAsync(FavoritesKey);
        if (string.IsNullOrEmpty(favoritesJson))
            return new List<string>();
            
        return JsonSerializer.Deserialize<List<string>>(favoritesJson);
    }
    
    public static async Task ToggleFavoriteAsync(Project project)
    {
        var favorites = await GetFavoriteProjectIdsAsync();
        
        if (project.IsFavorite && !favorites.Contains(project.ProjectID))
            favorites.Add(project.ProjectID);
        else if (!project.IsFavorite && favorites.Contains(project.ProjectID))
            favorites.Remove(project.ProjectID);
            
        string json = JsonSerializer.Serialize(favorites);
        await SecureStorage.SetAsync(FavoritesKey, json);
    }
    
    public static async Task LoadFavoritesIntoProjectsAsync(List<Project> projects)
    {
        var favorites = await GetFavoriteProjectIdsAsync();
        
        foreach (var project in projects)
        {
            project.IsFavorite = favorites.Contains(project.ProjectID);
        }
    }
}