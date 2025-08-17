using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExploradordePaises.Models;
using ExploradordePaises.Services;

namespace ExploradordePaises.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly PaisService _service;

    public ObservableCollection<Pais> Paises { get; } = new ObservableCollection<Pais>();

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(); }
    }

    public MainViewModel()
    {
        _service = new PaisService();
        CarregarPaises();
    }

    private async void CarregarPaises()
    {
        try
        {
            IsBusy = true;
            var lista = await _service.GetPaisesAsync();
            Paises.Clear();
            foreach (var p in lista.OrderBy(x => x.Name?.Common))
            {
                Paises.Add(p);
            }
        }
        catch
        {
            // aqui você pode logar ou tratar erros
        }
        finally
        {
            IsBusy = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
