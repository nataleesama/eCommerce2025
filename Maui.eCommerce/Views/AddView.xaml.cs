using eCommerce.Models;
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class AddView : ContentPage
{
	public AddView()
	{
		InitializeComponent();
    }

    public int ProductId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        (BindingContext as AddViewModel).Undo();
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void EnterClicked(object sender, EventArgs e)
    {
        var result = (BindingContext as AddViewModel).AddOrUpdate();
        if(result == true)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if(ProductId == 0)
        {
            BindingContext = new AddViewModel();
        }
        else
        {
            BindingContext = new AddViewModel(ProductServiceProxy.Current.GetById(ProductId));
        }
    }
}