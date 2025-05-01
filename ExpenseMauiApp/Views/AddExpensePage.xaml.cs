using ExpenseMauiApp.Models;
using ExpenseMauiApp.Services;


namespace ExpenseMauiApp.Views;

[QueryProperty(nameof(ProjectID), "ProjectID")]
[QueryProperty(nameof(ExpenseID), "ExpenseID")]
[QueryProperty(nameof(IsNew), "IsNew")]
public partial class AddExpensePage : ContentPage
{
    private CloudService _cloudService;
    private string _projectId;
    private int _expenseId;
    private bool _isNew = true;
    private Expense _currentExpense;
    
    public string PageTitle => _isNew ? "Add Expense" : "Edit Expense";
    
    public AddExpensePage()
    {
        InitializeComponent();
        _cloudService = new CloudService();
        
        // Set default values
        datePickerExpenseDate.Date = DateTime.Today;
        pickerCurrency.SelectedIndex = 1; // Pound Sterling
        pickerPaymentStatus.SelectedIndex = 0; // Pending
        pickerPaymentMethod.SelectedIndex = 0; // Cash
        pickerExpenseType.SelectedIndex = 0; // Travel
        
        BindingContext = this;
    }
    
    public string ProjectID
    {
        get => _projectId;
        set => _projectId = value;
    }
    
    public string ExpenseID
    {
        get => _expenseId.ToString();
        set
        {
            if (int.TryParse(value, out int id))
            {
                _expenseId = id;
                LoadExpense();
            }
        }
    }
    
    public string IsNew
    {
        get => _isNew.ToString();
        set
        {
            if (bool.TryParse(value, out bool isNew))
            {
                _isNew = isNew;
                btnDelete.IsVisible = !_isNew;
                OnPropertyChanged(nameof(PageTitle));
            }
        }
    }
    
    private async void LoadExpense()
    {
        if (_isNew) return;
        
        try
        {
            // In a real implementation, you would call the cloud service to get the expense
            // _currentExpense = await _cloudService.GetExpenseAsync(_expenseId);
            
            // For now, since we're focusing on the Add functionality, we'll just return
            await DisplayAlert("Info", "Editing expenses is not implemented yet.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load expense: {ex.Message}", "OK");
        }
    }
    
    private void SetPickerSelection(Picker picker, string value)
    {
        for (int i = 0; i < picker.Items.Count; i++)
        {
            if (picker.Items[i] == value)
            {
                picker.SelectedIndex = i;
                break;
            }
        }
    }
    
    private async void OnSaveExpenseClicked(object sender, EventArgs e)
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(entryExpenseID.Text) ||
            string.IsNullOrWhiteSpace(entryClaimant.Text) ||
            string.IsNullOrWhiteSpace(entryAmount.Text) ||
            !double.TryParse(entryAmount.Text, out double amount) ||
            amount <= 0 ||
            pickerExpenseType.SelectedIndex < 0 ||
            pickerCurrency.SelectedIndex < 0 ||
            pickerPaymentMethod.SelectedIndex < 0 ||
            pickerPaymentStatus.SelectedIndex < 0)
        {
            await DisplayAlert("Validation Error", "Please fill in all required fields", "OK");
            return;
        }
        
        try
        {
            var expense = new Expense
            {
                ExpenseType = pickerExpenseType.SelectedItem.ToString(),
                ExpenseID = entryExpenseID.Text,
                Claimant = entryClaimant.Text,
                Amount = amount,
                Currency = pickerCurrency.SelectedItem.ToString(),
                PaymentMethod = pickerPaymentMethod.SelectedItem.ToString(),
                PaymentStatus = pickerPaymentStatus.SelectedItem.ToString(),
                ExpenseDate = datePickerExpenseDate.Date,
                Description = editorDescription.Text ?? string.Empty,
                Location = editorLocation.Text ?? string.Empty,
                ProjectID = _projectId
            };
            
            bool success = await _cloudService.AddExpenseAsync(expense);
            
            if (success)
            {
                await DisplayAlert("Success", "Expense added successfully", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "Failed to save expense", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    
    private async void OnDeleteExpenseClicked(object sender, EventArgs e)
    {
        // Since we're focusing on adding expenses, this is not implemented
        await DisplayAlert("Info", "Deleting expenses is not implemented yet.", "OK");
    }
    
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}