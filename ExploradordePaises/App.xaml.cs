using Microsoft.Maui;

namespace ExploradordePaises;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    // Para evitar aviso sobre MainPage obsoleto em versões recentes
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(new MainPage()));
    }
}
