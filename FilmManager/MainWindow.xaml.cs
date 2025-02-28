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
    private readonly ViewMovieViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        // Obtener servicios desde la configuración de la aplicación
        _navigationService = App.Current.Services.GetService<INavigationService>() ??
                             throw new InvalidOperationException("NavigationService no está disponible");
        _viewModel = App.Current.Services.GetService<ViewMovieViewModel>() ??
                     new ViewMovieViewModel();

        // Configurar la navegación
        _navigationService.SetNavigationControl(NavigationView);

        // Asignar el ViewModel a la vista
        DataContext = _viewModel;
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Navegar a la página inicial
        NavigationView.Navigate(typeof(WelcomePage));

        // Cargar datos después de que la ventana esté lista
        _viewModel.LoadFilms();
    }
    
}