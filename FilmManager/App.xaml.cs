using System.Windows;
using FilmManager.Core.Services.Films;
using FilmManager.Core.Services.Films.Network;
using FilmManager.Core.Services.Navigate;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = InitServices();
        InitializeComponent();
    }

    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;

    private IServiceProvider InitServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IFilmService, FilmService>();
        services.AddSingleton<FilmServiceNetwork>();

        return services.BuildServiceProvider();
    }
}