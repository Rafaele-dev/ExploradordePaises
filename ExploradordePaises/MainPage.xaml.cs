using ExploradordePaises.Models;
using ExploradordePaises.Views;

namespace ExploradordePaises;

public partial class MainPage : ContentPage
{
    private ViewModels.MainViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new ViewModels.MainViewModel();
        BindingContext = _viewModel;
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

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            var searchText = e.NewTextValue?.Trim() ?? string.Empty;
            _viewModel.FiltrarPaises(searchText);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro na pesquisa: {ex.Message}");
        }
    }

    private void OnClearSearchClicked(object sender, EventArgs e)
    {
        try
        {
            var searchEntry = this.FindByName<Entry>("SearchEntry");
            if (searchEntry != null)
            {
                searchEntry.Text = string.Empty;
                _viewModel.FiltrarPaises(string.Empty);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erro ao limpar pesquisa: {ex.Message}");
        }
    }
}