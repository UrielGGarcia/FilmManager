using System.Configuration;
using System.Data;
using System.Windows;
using FilmManager.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;

    public App()
    {
        Services = InitServices();
        InitializeComponent();
    }

    private IServiceProvider InitServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, NavigationService>();

        return services.BuildServiceProvider();
    }
}