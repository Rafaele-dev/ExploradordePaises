using System.Linq;
using ExploradordePaises.Models;
using ExploradordePaises.ViewModels;
using ExploradordePaises.Views;

namespace ExploradordePaises;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var pais = e.CurrentSelection.FirstOrDefault() as Pais;
        if (pais == null) return;

        await Navigation.PushAsync(new CountryDetailsPage { BindingContext = pais });

        // limpa seleção pra poder clicar novamente no mesmo item depois
        if (sender is CollectionView cv)
            cv.SelectedItem = null;
    }
}
