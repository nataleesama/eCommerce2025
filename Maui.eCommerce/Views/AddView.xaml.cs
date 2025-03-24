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
		Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void EnterClicked(object sender, EventArgs e)
    {
        var name = (BindingContext as AddViewModel)?.Name;
        ProductServiceProxy.Current.AddOrUpdate(new Product {Name = name});
        Shell.Current.GoToAsync("//InventoryManagement");
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