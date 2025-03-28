using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
        BindingContext = new ShopViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshLists();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.AddToCart();
    }

    private void RemoveClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.RemoveFromCart();
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Receipt");
    }
}