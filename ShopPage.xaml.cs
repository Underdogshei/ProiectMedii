using ProiectMedii.Models;
namespace ProiectMedii;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;
        // var locations = await Geocoding.GetLocationsAsync(address);
        var options = new MapLaunchOptions { Name = "Service-ul preferat" };
        var shoplocation = new Location(46.169715, 23.942380);
        //var myLocation = await Geolocation.GetLocationAsync();
        var myLocation = new Location(46.169715, 23.942380); //pentru Windows Machine
        var distance = myLocation.CalculateDistance(shoplocation, DistanceUnits.Kilometers);
        await Map.OpenAsync(shoplocation, options);
    }
}