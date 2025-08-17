using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExploradordePaises.Models;
using ExploradordePaises.Services;

namespace ExploradordePaises.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly PaisService _service;
    private ObservableCollection<Pais> _paisesFiltrados = new();
    private bool _isBusy;
    private string _filtroAtual = string.Empty;

    public ObservableCollection<Pais> Paises { get; } = new ObservableCollection<Pais>();

    public ObservableCollection<Pais> PaisesFiltrados
    {
        get => _paisesFiltrados;
        set
        {
            _paisesFiltrados = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        _service = new PaisService();
        System.Diagnostics.Debug.WriteLine("MainViewModel: Construtor chamado");
        CarregarPaises();
    }

    private async void CarregarPaises()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel: Iniciando carregamento de países");
            IsBusy = true;

            var lista = await _service.GetPaisesAsync();
            System.Diagnostics.Debug.WriteLine($"MainViewModel: Recebidos {lista?.Count ?? 0} países da API");

            Paises.Clear();

            if (lista != null && lista.Any())
            {
                var paisesOrdenados = lista.OrderBy(x => x.Name?.Common).ToList();

                foreach (var p in paisesOrdenados)
                {
                    Paises.Add(p);
                }

                System.Diagnostics.Debug.WriteLine($"MainViewModel: Adicionados {Paises.Count} países à lista");

                PaisesFiltrados = new ObservableCollection<Pais>(Paises);
                System.Diagnostics.Debug.WriteLine($"MainViewModel: Lista filtrada inicializada com {PaisesFiltrados.Count} países");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("MainViewModel: ERRO - Lista de países está vazia ou nula");
                PaisesFiltrados = new ObservableCollection<Pais>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"MainViewModel: ERRO ao carregar países: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"MainViewModel: Stack trace: {ex.StackTrace}");

            PaisesFiltrados = new ObservableCollection<Pais>();
        }
        finally
        {
            IsBusy = false;
            System.Diagnostics.Debug.WriteLine($"MainViewModel: Carregamento finalizado. IsBusy = {IsBusy}");
        }
    }

    public void FiltrarPaises(string filtro)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"MainViewModel: Filtrando países com termo: '{filtro}'");
            _filtroAtual = filtro;

            if (string.IsNullOrWhiteSpace(filtro))
            {
                PaisesFiltrados = new ObservableCollection<Pais>(Paises);
                System.Diagnostics.Debug.WriteLine($"MainViewModel: Filtro vazio - mostrando todos {PaisesFiltrados.Count} países");
            }
            else
            {
                var filtroLower = filtro.ToLowerInvariant();

                var paisesFiltrados = Paises.Where(pais =>
                    (pais.Name?.Common?.ToLowerInvariant().Contains(filtroLower) ?? false) ||
                    (pais.Name?.Official?.ToLowerInvariant().Contains(filtroLower) ?? false) ||
                    (pais.CapitalDisplay?.ToLowerInvariant().Contains(filtroLower) ?? false) ||
                    (pais.Region?.ToLowerInvariant().Contains(filtroLower) ?? false) ||
                    (pais.SubregionDisplay?.ToLowerInvariant().Contains(filtroLower) ?? false)
                ).ToList();

                PaisesFiltrados = new ObservableCollection<Pais>(paisesFiltrados);
                System.Diagnostics.Debug.WriteLine($"MainViewModel: Filtrados {PaisesFiltrados.Count} países com o termo '{filtro}'");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"MainViewModel: ERRO ao filtrar países: {ex.Message}");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}