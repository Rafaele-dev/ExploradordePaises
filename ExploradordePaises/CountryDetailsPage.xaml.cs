using ExploradordePaises.Models;

namespace ExploradordePaises.Views;

public partial class CountryDetailsPage : ContentPage
{
    public CountryDetailsPage()
    {
        InitializeComponent();
    }

    public CountryDetailsPage(Pais pais) : this()
    {
        BindingContext = pais;
    }
}