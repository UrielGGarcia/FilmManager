using System.Windows;
using FilmManager.Core.Services.Navigate;
using FilmManager.ViewMovie;
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
                     new ViewMovieViewModel(
                         "Server=192.168.56.20;Port=3306;Database=sakila;User=uriel;Password=uriel.120;SslMode=None;");

        // Configurar la navegación
        _navigationService.SetNavigationControl(NavigationView);

        // Asignar el ViewModel a la vista
        DataContext = _viewModel;
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        // Navegar a la página inicial
        NavigationView.Navigate(typeof(ViewMoviePage));

        // Cargar datos después de que la ventana esté lista
        _viewModel.LoadFilms();
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}