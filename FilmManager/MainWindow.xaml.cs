using System.Windows;
using FilmManager.Core.Services.Navigate;
using FilmManager.ViewMovie;
using FilmManager.Welcome;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly INavigationService _navigationService;

    public MainWindow()
    {
        InitializeComponent();

        // Obtener servicios desde la configuración de la aplicación
        _navigationService = App.Current.Services.GetService<INavigationService>() ??
                             throw new InvalidOperationException("NavigationService no está disponible");
        

        // Configurar la navegación
        _navigationService.SetNavigationControl(NavigationView);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Navegar a la página inicial
        NavigationView.Navigate(typeof(WelcomePage));

    }
    
}