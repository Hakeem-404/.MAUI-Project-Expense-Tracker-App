using System.Text.Json.Serialization;

namespace ExpenseMauiApp.Models;

public class Project
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string ProjectName { get; set; }

    [JsonPropertyName("projectId")]
    public string ProjectID { get; set; }

    [JsonPropertyName("manager")]
    public string Manager { get; set; }
    
    [JsonPropertyName("status")]
    public string ProjectStatus { get; set; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("budget")]
    public double Budget { get; set; }

    [JsonPropertyName("description")]
    public string ProjectDesc { get; set; }

    [JsonPropertyName("specialRequirements")]
    public string SpecialReq { get; set; }

    [JsonPropertyName("clientInfo")]
    public string ClientInfo { get; set; }

    [JsonPropertyName("isFavorite")]
    public bool IsFavorite { get; set; }
}
