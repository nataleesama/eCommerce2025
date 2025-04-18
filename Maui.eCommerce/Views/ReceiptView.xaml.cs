using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ReceiptView : ContentPage
{
    public ReceiptView()
    {
        InitializeComponent();
        BindingContext = new ReceiptViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        (BindingContext as ReceiptViewModel)?.DropCart();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ReceiptViewModel)?.RefreshReceipt();
    }
}