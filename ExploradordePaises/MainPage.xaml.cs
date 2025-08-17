using ExploradordePaises.Models;
using ExploradordePaises.Views;

namespace ExploradordePaises;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.MainViewModel();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection.FirstOrDefault() is Pais paisSelecionado)
            {
                var detailsPage = new CountryDetailsPage
                {
                    BindingContext = paisSelecionado
                };

                await Navigation.PushAsync(detailsPage);

                ((CollectionView)sender).SelectedItem = null;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao navegar: {ex.Message}");

            await DisplayAlert("Erro", "Erro ao abrir detalhes do país", "OK");
        }
    }
}