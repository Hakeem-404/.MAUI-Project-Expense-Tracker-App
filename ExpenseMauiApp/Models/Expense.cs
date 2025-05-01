namespace ExpenseMauiApp.Models;

public class Expense
{
    
    public int Id { get; set; }
    public string ExpenseType { get; set; }
    public string ExpenseID { get; set; }
    public double Amount { get; set; }
    public string Claimant { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public string Currency { get; set; }
    public string ProjectID { get; set; }
}
