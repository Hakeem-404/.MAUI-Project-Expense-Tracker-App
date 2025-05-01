using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ExpenseMauiApp.Models;

namespace ExpenseMauiApp.Services
{
    public class CloudService
    {
        private readonly HttpClient _httpClient;
        // Base URL for your Firebase Realtime Database. 
        // Note: The .json extension must be appended to each endpoint.
        private readonly string _baseUrl = "https://projectexpensetracker-953eb-default-rtdb.firebaseio.com/";

        public CloudService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Retrieves all projects from the Firebase Realtime Database.
        /// </summary>
        public async Task<List<Project>> GetProjectsAsync()
    {
        try
        {
            string url = $"{_baseUrl}projects.json";
            
            // Firebase returns objects as dictionary with keys
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, Project>>(url);
            
            if (response != null)
            {
                // Convert dictionary to list
                return response.Values.ToList();
            }
            
            return new List<Project>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting projects: {ex.Message}");
            return new List<Project>();
        }
    }

        /// <summary>
        /// Searches projects by name (case‑insensitive).
        /// For simplicity, this example fetches all projects and filters them locally.
        /// </summary>
        public async Task<List<Project>> SearchProjectsAsync(string query)
        {
            try
            {
                var projects = await GetProjectsAsync();
                // Filter projects by ProjectName containing the query text.
                var filtered = projects.FindAll(p => 
                    !string.IsNullOrWhiteSpace(p.ProjectName) &&
                    p.ProjectName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                return filtered;
            }
            catch (Exception ex)
            {
                // Optionally log the error: ex.Message
                return new List<Project>();
            }
        }

        /// <summary>
        /// Adds a new expense to the Firebase Realtime Database.
        /// </summary>
        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            try
            {
                // Endpoint for expenses. Again, note the .json extension.
                string url = $"{_baseUrl}expenses.json";
                var response = await _httpClient.PostAsJsonAsync(url, expense);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Optionally log the error: ex.Message
                return false;
            }
        }
    }
}
