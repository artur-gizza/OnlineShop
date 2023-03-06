namespace ShoppingCart.Models.ViewModels;

public class ErrorViewModel
{
    public string Message { get; set; }

    public bool ShowMessage => !string.IsNullOrEmpty(Message);
}

