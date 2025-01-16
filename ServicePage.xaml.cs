using ProiectMedii.Models;

namespace ProiectMedii;

public partial class ServicePage : ContentPage
{
    ServiceList sl;
    public ServicePage(ServiceList slist)
    {
        InitializeComponent();
        sl = slist;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var service = (Service)BindingContext;
        await App.Database.SaveServiceAsync(service);
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var service = listView.SelectedItem as Service;
        await App.Database.DeleteServiceAsync(service);
        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        listView.ItemsSource = await App.Database.GetServicesAsync();
    }

    async void OnChooseButtonClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new ServicePage((ServiceList)this.BindingContext)
        {
            BindingContext = new Service()
        });

    }

    async void OnAddButtonClicked(object sender, EventArgs e)
    {

        Service p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Service;
            var lp = new ListService()
            {
                ServiceListID = sl.ID,
                ServiceID = p.ID
            };
            await App.Database.SaveListServiceAsync(lp);
            p.ListServices = new List<ListService> { lp };

            await Navigation.PopAsync();
        }
    }
}