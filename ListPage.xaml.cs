using ProiectMedii.Models;
namespace ProiectMedii;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveServiceListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ServiceList)BindingContext;
        await App.Database.DeleteServiceListAsync(slist);
        await Navigation.PopAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ServicePage((ServiceList)this.BindingContext)
        {
            BindingContext = new Service()
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var servicel = (ServiceList)BindingContext;

        listView.ItemsSource = await App.Database.GetListServicesAsync(servicel.ID);
    }
}